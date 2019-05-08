using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.Extensions;
using log4net;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Earth.Ear.Therapy.FanFoot.External
{
    public interface ITeamsWeeklyDocuments
    {
        string Create();
    }

    public class TeamsWeeklyDocuments : ITeamsWeeklyDocuments
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Document _document = null;

        private int _seasonId = -1;

        private int _weekOffset = -1;

        private ISeasonsRepository SeasonsRepository { get; }

        private ITeamsRepository TeamsRepository { get; }

        private IPlayersRepository PlayersRepository { get; }

        public TeamsWeeklyDocuments(ISeasonsRepository seasonsRepository, ITeamsRepository teamsRepository, IPlayersRepository playersRepository)
        {
            SeasonsRepository = seasonsRepository;
            TeamsRepository = teamsRepository;
            PlayersRepository = playersRepository;
        }

        private void AddTeamPlayers(Table teamTable, TeamEntity teamEntity, IEnumerable<PlayerEntity> playerEntities)
        {
            int lastElementTypeId = -1;

            TableRow tableRow = null;

            Paragraph innerPara = null;

            TextRange textRange = null;

            foreach (var playerEntity in playerEntities)
            {
                if (playerEntity.PremierLeagueElementTypeId != lastElementTypeId)
                {
                    tableRow = teamTable.AddRow(5);

                    innerPara = tableRow.Cells[0].AddParagraph();
                    textRange = innerPara.AppendText($"{teamEntity.TeamName} {playerEntity.PlayerTypeNamePlural}");
                    innerPara.Format.HorizontalAlignment = HorizontalAlignment.Left;
                    textRange.CharacterFormat.FontName = "Calibri";
                    textRange.CharacterFormat.FontSize = 14;
                    textRange.CharacterFormat.Italic = true;

                    lastElementTypeId = playerEntity.PremierLeagueElementTypeId;
                }

                tableRow = teamTable.AddRow(5);

                innerPara = tableRow.Cells[0].AddParagraph();
                textRange = innerPara.AppendText($"{playerEntity.SecondName} ({playerEntity.FirstName})");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[1].AddParagraph();
                textRange = innerPara.AppendText($"£{playerEntity.NowCost / 10.00M}");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[2].AddParagraph();
                textRange = innerPara.AppendText($"{playerEntity.TotalPoints}");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[3].AddParagraph();
                textRange = innerPara.AppendText(playerEntity.Status);
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[4].AddParagraph();
                textRange = innerPara.AppendText(playerEntity.News);
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;
            }
        }

        private void AddTeam(TeamEntity teamEntity)
        {
            var teamSection = _document.AddSection();
            var teamNamePara = teamSection.AddParagraph();
            var textRange = teamNamePara.AppendText(teamEntity.TeamName);
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 18;
            textRange.CharacterFormat.Bold = true;

            var teamTable = teamSection.AddTable(true);

            var tableRow = teamTable.AddRow(5);

            var innerPara = tableRow.Cells[0].AddParagraph();
            tableRow.Cells[0].SetCellWidth(25, CellWidthType.Percentage);
            textRange = innerPara.AppendText("Player");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Bold = true;

            innerPara = tableRow.Cells[1].AddParagraph();
            tableRow.Cells[1].SetCellWidth(12.5f, CellWidthType.Percentage);
            textRange = innerPara.AppendText("Cost (Millions)");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Bold = true;

            innerPara = tableRow.Cells[2].AddParagraph();
            tableRow.Cells[2].SetCellWidth(12.5f, CellWidthType.Percentage);
            textRange = innerPara.AppendText("Points");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Bold = true;

            innerPara = tableRow.Cells[3].AddParagraph();
            tableRow.Cells[3].SetCellWidth(10, CellWidthType.Percentage);
            textRange = innerPara.AppendText("Status");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Bold = true;

            innerPara = tableRow.Cells[4].AddParagraph();
            tableRow.Cells[4].SetCellWidth(40, CellWidthType.Percentage);
            textRange = innerPara.AppendText("News");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Bold = true;

            var playerEntities = PlayersRepository
                .Get(teamEntity.TeamId);

            AddTeamPlayers(teamTable, teamEntity, playerEntities);

#if false
            var teamPlayers = GetTeamPlayers(teamBdo);

            foreach (var playerTypePair in _fantasyFootballBdo.PlayerTypes)
            {
                AddTeamPlayerType(teamTable, teamBdo, playerTypePair.Value, teamPlayers);
            }
#endif
        }

        public string Create()
        {
            string resultFile = null;

            var utcNow = DateTime.UtcNow;

            var seasonEntity = SeasonsRepository.Get(utcNow);

            if (seasonEntity == null)
                throw new Exception($"There is no Season covering the Date = {utcNow:yyyy-MMM-dd}.");

            _seasonId = seasonEntity.SeasonId;

            _weekOffset = utcNow.GetIsoWeekOfYear();

            var firstResultsSet = TeamsRepository.GetFirstOrDefault(entity => (entity.SeasonId == _seasonId) && (entity.WeekOffset == _weekOffset));

            if (firstResultsSet == null)
                throw new Exception($"There is no Weekly Results Set covering the Date = {utcNow:yyyy-MMM-dd}, otherwise known as Week Offset = {_weekOffset}.");

            _document = new Document();

            // TODO: .OrderBy(PremierLeagueTeamId) ASC.
            var teamEntities = TeamsRepository
                .GetMany(entity => (entity.SeasonId == _seasonId) && (entity.WeekOffset == _weekOffset));

            foreach (var teamEntity in teamEntities)
            {
                AddTeam(teamEntity);
            }

            HeaderFooter footer = _document.Sections[0].HeadersFooters.Footer;
            Paragraph footerParagraph = footer.AddParagraph();
            var textRange = footerParagraph.AppendText("Page ");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange = footerParagraph.AppendField("page number", FieldType.FieldPage);
            textRange.CharacterFormat.FontName = "Calibri";
            //footerParagraph.AppendText(" of ");
            //footerParagraph.AppendField("number of pages", FieldType.FieldSectionPages);
            footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;

            resultFile = $"Therapy Fantasy Football - {DateTime.UtcNow:yyyy-MMM-dd}.docx";

            _document.SaveToFile($"wwwroot\\{resultFile}", FileFormat.Docx2013);

            return resultFile;
        }
    }
}
