using System;
using System.Collections.Generic;
using Earth.Ear.Therapy.FanFoot.BusinessDataObjects;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace Earth.Ear.Therapy.FanFoot.External
{
    public class TeamsDocument
    {
        private Document _document = null;

        private FantasyFootballBdo _fantasyFootballBdo = null;

        private Dictionary<int, Element> GetSpecificTeamPlayers(ElementType playerType, Dictionary<int, Element> teamPlayers)
        {
            var specificTeamPlayers = new Dictionary<int, Element>();

            // TODO: Figure out how to use LINQ to do this.

            foreach (var pair in teamPlayers)
            {
                if (pair.Value.element_type == playerType.id)
                {
                    specificTeamPlayers.Add(pair.Key, pair.Value);
                }
            }

            return specificTeamPlayers;
        }

        private void AddTeamPlayerType(Table teamTable, Team team, ElementType playerType, Dictionary<int, Element> players)
        {
            var specificTeamPlayerTypes = GetSpecificTeamPlayers(playerType, players);

            var tableRow = teamTable.AddRow(5);

            var innerPara = tableRow.Cells[0].AddParagraph();
            TextRange textRange = innerPara.AppendText($"{team.name} {playerType.plural_name}");
            innerPara.Format.HorizontalAlignment = HorizontalAlignment.Left;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 14;
            textRange.CharacterFormat.Italic = true;

            foreach (var pair in specificTeamPlayerTypes)
            {
                tableRow = teamTable.AddRow(5);

                innerPara = tableRow.Cells[0].AddParagraph();
                textRange = innerPara.AppendText($"{pair.Value.second_name} ({pair.Value.first_name})");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[1].AddParagraph();
                textRange = innerPara.AppendText($"£{pair.Value.now_cost / 10.00M}");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[2].AddParagraph();
                textRange = innerPara.AppendText($"{pair.Value.total_points}");
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[3].AddParagraph();
                textRange = innerPara.AppendText(pair.Value.status);
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;

                innerPara = tableRow.Cells[4].AddParagraph();
                textRange = innerPara.AppendText(pair.Value.news);
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.FontSize = 12;
             }
        }

        private Dictionary<int, Element> GetTeamPlayers(Team teamBdo)
        {
            var teamPlayers = new Dictionary<int, Element>();

            // TODO: Figure out how to use LINQ to do this.

            foreach (var pair in _fantasyFootballBdo.Players)
            {
                if (pair.Value.team == teamBdo.id)
                {
                    teamPlayers.Add(pair.Key, pair.Value);
                }
            }

            return teamPlayers;
        }

        private void AddTeam(Team teamBdo)
        {
            var teamSection = _document.AddSection();
            var teamNamePara = teamSection.AddParagraph();
            var textRange = teamNamePara.AppendText(teamBdo.name);
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.FontSize = 18;
            textRange.CharacterFormat.Bold = true;

            var teamPlayers = GetTeamPlayers(teamBdo);

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

            foreach (var playerTypePair in _fantasyFootballBdo.PlayerTypes)
            {
                AddTeamPlayerType(teamTable, teamBdo, playerTypePair.Value, teamPlayers);
            }
        }

        public string Create(FantasyFootballBdo fantasyFootballBdo)
        {
            string fileName = null;

            _fantasyFootballBdo = fantasyFootballBdo;

            _document = new Document();
            
            // TODO: REVIEW: The following attempt at setting the font globally on a Document basis. 
            ParagraphStyle s1;
            s1 = _document.AddParagraphStyle("textStyle");
            s1.CharacterFormat.FontName = "Arial";

            foreach (var teamBdoPair in _fantasyFootballBdo.Teams)
            {
                AddTeam(teamBdoPair.Value);
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

            fileName = $"OT Fantasy Football - {DateTime.UtcNow:yyyy-MMM-dd}.docx";

            _document.SaveToFile($"wwwroot\\{fileName}", FileFormat.Docx2013);

            return fileName;
        }
    }
}
