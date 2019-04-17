--------------------------------------------------------------------------------
--
-- Created: Éamonn A. Duffy, 12-Apr-2019.
--
--------------------------------------------------------------------------------

DROP TABLE IF EXISTS Player;
DROP TABLE IF EXISTS Team;
DROP TABLE IF EXISTS Season;
DROP TABLE IF EXISTS DatabaseVersion;

--------------------------------------------------------------------------------

CREATE TABLE DatabaseVersion
(
    DatabaseVersionId           Integer PRIMARY KEY NOT NULL,
    Major                       Integer NOT NULL,
    Minor                       Integer NOT NULL,
    Build                       Integer NOT NULL,
    ServicePack                 Integer NOT NULL,
    Description                 Text NOT NULL,
    CreatedDateTimeUtc          Text NOT NULL
);

--------------------------------------------------------------------------------

CREATE TABLE Season
(
    SeasonId                    Integer PRIMARY KEY NOT NULL,
    BeginDateTimeUtc            Text NOT NULL,
    EndDateTimeUtc              Text NOT NULL,
    CreatedDateTimeUtc          Text NOT NULL
);

--------------------------------------------------------------------------------

CREATE TABLE Team
(
    TeamId                      Integer PRIMARY KEY NOT NULL,
    SeasonId                    Integer NOT NULL,
    WeekOffset                  Integer NOT NULL,
    PremierLeagueTeamId         Integer NOT NULL,
    TeamName                    Text NULL,
    CreatedDateTimeUtc          Text NOT NULL,

    FOREIGN KEY (SeasonId) REFERENCES Season(SeasonId)
);

--------------------------------------------------------------------------------

CREATE TABLE Player
(
    PlayerId                    Integer PRIMARY KEY NOT NULL,
    TeamId                      Integer NOT NULL,
    PremierLeagueElementId      Integer NOT NULL,
    PremierLeagueElementTypeId  Integer NOT NULL,
    FirstName                   Text NULL,
    SecondName                  Text NULL,
    NowCost                     Text NOT NULL,
    TotalPoints                 Integer NOT NULL,
    Status                      Text NOT NULL,
    News                        Text NULL,
    CreatedDateTimeUtc          Text NOT NULL,

    FOREIGN KEY (TeamId) REFERENCES Team(TeamId)
);

--------------------------------------------------------------------------------

INSERT INTO DatabaseVersion
    (Major, Minor, Build, ServicePack, Description, CreatedDateTimeUtc)
VALUES
    (0, 0, 0, 0, 'V0.0', datetime('now'));

--------------------------------------------------------------------------------

