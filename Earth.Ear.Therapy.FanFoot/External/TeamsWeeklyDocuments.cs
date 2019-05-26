using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.Extensions;
using Earth.Ear.Therapy.FanFoot.Helpers;
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

            int totalPoints = 0;
            int deltaPoints = 0;

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

                totalPoints = playerEntity.TotalPoints;

                var mostRecentPlayerEntity = PlayersRepository.GetMostRecent(teamEntity.SeasonId, playerEntity.PremierLeagueElementId);

                if (mostRecentPlayerEntity != null)
                {
                    deltaPoints = totalPoints - mostRecentPlayerEntity.TotalPoints;
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
                if (deltaPoints == 0)
                {
                    textRange = innerPara.AppendText($"{totalPoints}");
                }
                else
                {
                    textRange = innerPara.AppendText($"{totalPoints} ({deltaPoints})");
                }
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
                .GetAll(teamEntity.TeamId);

            AddTeamPlayers(teamTable, teamEntity, playerEntities);
        }

        public string Create()
        {
            string resultFile = null;

            var utcNow = DateTime.UtcNow;

            var seasonEntity = SeasonsRepository.Get(utcNow);

            if (seasonEntity == null)
                throw new Exception($"There is no Season covering the Date = {utcNow:yyyy-MMM-dd}.");

            _seasonId = seasonEntity.SeasonId;

            _weekOffset = DateTimeHelper.GetWeekOffset(seasonEntity, utcNow);

            var firstResultsSet = TeamsRepository.GetFirstOrDefault(entity => (entity.SeasonId == _seasonId) && (entity.WeekOffset == _weekOffset));

            if (firstResultsSet == null)
                throw new Exception($"There is no Weekly Results Set covering the Date = {utcNow:yyyy-MMM-dd}, otherwise known as Week Offset = {_weekOffset}.");

            _document = new Document();

            var teamEntities = TeamsRepository
                .GetAll(_seasonId, _weekOffset);

            foreach (var teamEntity in teamEntities)
            {
                AddTeam(teamEntity);
            }

            HeaderFooter footer = _document.Sections[0].HeadersFooters.Footer;
            Paragraph footerParagraph = footer.AddParagraph();
            var textRange = footerParagraph.AppendText($"Season Id = {_seasonId} : Week Offset = {_weekOffset} : Page ");
            textRange.CharacterFormat.FontName = "Calibri";
            textRange = footerParagraph.AppendField("page number", FieldType.FieldPage);
            textRange.CharacterFormat.FontName = "Calibri";
            footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

            resultFile = $"Therapy Fantasy Football - {DateTime.UtcNow:yyyy-MMM-dd}.docx";

            _document.SaveToFile($"wwwroot\\{resultFile}", FileFormat.Docx2013);

            return resultFile;
        }
    }
}
