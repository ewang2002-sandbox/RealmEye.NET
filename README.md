# RealmEyeNET
An unofficial RealmEye API designed for advanced player verification. 

NOTE: At the moment, this API is in testing and IS expected to break. Do not rely on this API for production use. 

## Purpose
<details>
<summary>Click Here</summary>
<br>
There are several other hosted APIs online; however, those APIs have either reached their end of life or are approaching it.
- Tiffit's API: Taken down on June 2nd, 2020.
	- Reason: "I've decided to stop maintaining the API since it stopped being something that I did for fun and more of a 'job.'"
- [Nightfirecat's API](https://github.com/Nightfirecat/RealmEye-API): Pretty outdated and expected to reach end-of-life soon (from the developer himself).

While both APIs have done an amazing job, the developers that have worked on these APIs have sadly moved on to pursue other interests. I have decided, then, to write my own API so I can continue to use it for my [bot](https://github.com/DungeoneerExalt/ZeroRaidBot/).
</details>

## Technologies
- .NET Core 3.1
- ASP.NET

## Project Template
- [RealmEyeApi](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeApi): The ASP.NET project that will be used to host the RealmEye API.
- [RealmEyeNET](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeNET): The .NET Core project containing code that scrapes RealmEye's website. 
- [RealmEyeTest](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeTest): A simple Console application designed solely for testing the RealmEyeNET project.

## API Endpoints
<details>
<summary>Click Here!</summary>
<br>
There are several endpoints that allow you to access various sections of someone's RealmEye profile. Upon sending a request, the API will scrape data from RealmEye and return the scrapped data in a JSON format.

The base URL is shown below.
```
api/realmeye/
```

Below are all available endpoints. For the examples, I will be using the IGN `Opre`. Do note that the examples may be shortened.


### Basic Data
```
basics/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- All the information found in the top-left corner of a person's RealmEye profile.

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "name": "Opre",
    "characterCount": 3,
    "skins": 16,
    "fame": 83,
    "exp": 18915,
    "rank": 71,
    "accountFame": 16612,
    "guild": "Exotics",
    "guildRank": "Founder",
    "firstSeen": null,
    "created": "~8 years and 107 days ago",
    "lastSeen": "2020-08-14 03:26:36 at USWest3 as Huntress",
    "description": []
}
```
</details>


### Character Data
```
char/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- All of the player's active characters (found on RealmEye). Note that RealmEye purposely shows the "best" character for each class; in other words, if a player has two characters of the same class, RealmEye will only display the best of the two characters. 

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "characters": [
        {
            "characterType": "Huntress",
            "level": 1,
            "classQuestsCompleted": 5,
            "fame": 0,
            "experience": 0,
            "place": 9032,
            "equipmentData": [
                "Bow of Covert Havens T12",
                "Giantcatcher Trap T6",
                "Hydra Skin Armor T13",
                "Ring of Exalted Health T5"
            ],
            "hasBackpack": false,
            "stats": {},
            "statsMaxed": 0
        },
        {
            "characterType": "Ninja",
            "level": 20,
            "classQuestsCompleted": 3,
            "fame": 65,
            "experience": 18915,
            "place": 6678,
            "equipmentData": [
                "Doku No Ken UT",
                "Doom Circle T6",
                "Harlequin Armor UT",
                "Frimarra UT"
            ],
            "hasBackpack": true,
            "stats": {},
            "statsMaxed": 1
        },
        {
            "characterType": "Trickster",
            "level": 20,
            "classQuestsCompleted": 4,
            "fame": 18,
            "experience": 0,
            "place": 8467,
            "equipmentData": [
                "Steel Dagger T0",
                "Decoy Prism T0",
                "Coral Silk Armor UT",
                "Ring of the Nile UT"
            ],
            "hasBackpack": true,
            "stats": {},
            "statsMaxed": 0
        }
    ]
}
```
</details>


### Pet Data
```
pets/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- All of the player's pets. 

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "pets": [
        {
            "activePetSkinId": 9173,
            "name": "Golden Sagitt...",
            "rarity": "Legendary",
            "family": "Humanoid",
            "place": 16902,
            "petAbilities": [
                {
                    "isUnlocked": true,
                    "abilityName": "Heal",
                    "level": 81,
                    "isMaxed": false
                },
                {
                    "isUnlocked": true,
                    "abilityName": "Magic Heal",
                    "level": 77,
                    "isMaxed": false
                },
                {
                    "isUnlocked": true,
                    "abilityName": "Savage",
                    "level": 61,
                    "isMaxed": false
                }
            ],
            "maxLevel": 85
        },
        {
            "activePetSkinId": 32603,
            "name": "Lil\u0026apos; Cyclops",
            "rarity": "Rare",
            "family": "Spooky",
            "place": -1,
            "petAbilities": [
                {
                    "isUnlocked": true,
                    "abilityName": "Heal",
                    "level": 57,
                    "isMaxed": false
                },
                {
                    "isUnlocked": true,
                    "abilityName": "Electric",
                    "level": 55,
                    "isMaxed": false
                },
                {
                    "isUnlocked": false,
                    "abilityName": "Magic Heal",
                    "level": -1,
                    "isMaxed": false
                }
            ],
            "maxLevel": 70
        }
    ]
}
```
</details>


### Graveyard Data
```
graveyard/{name}/{amount?}
```

**Parameter(s)**
- name (string): The person to get information for. 
- amount (int?): The maximum number of entries to achieve. By default, this is set to `-1` (all entries). 

**Return(s)**
- The player's graveyard. If you do not specify an amount, the API will go through ALL of the player's dead characters. Depending on who you look up, the API can take upwards of 4 minutes.

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "graveyardCount": 1010,
    "graveyard": [
        {
            "diedOn": "2020-08-14T03:22:31Z",
            "character": "Samurai",
            "level": 20,
            "baseFame": 1250,
            "totalFame": 3364,
            "experience": 2108042,
            "equipment": [
                "Masamune T12",
                "Royal Wakizashi T6",
                "Acropolis Armor T13",
                "Ring of Exalted Health T5"
            ],
            "maxedStats": 7,
            "killedBy": "Actual Ent Ancient",
            "hadBackpack": false
        },
        {
            "diedOn": "2020-08-11T18:53:12Z",
            "character": "Wizard",
            "level": 20,
            "baseFame": 82,
            "totalFame": 156,
            "experience": 46101,
            "equipment": [
                "Staff of the Cosmic Whole T12",
                "Elemental Detonation Spell T6",
                "Robe of the Grand Sorcerer T13",
                "Ring of Exalted Health T5"
            ],
            "maxedStats": 0,
            "killedBy": "Horrid Reaper",
            "hadBackpack": true
        },
        {
            "diedOn": "2020-08-09T21:52:24Z",
            "character": "Ninja",
            "level": 10,
            "baseFame": 7,
            "totalFame": 7,
            "experience": 4682,
            "equipment": [
                "Line Kutter Katana T4",
                "Four-Point Star T1",
                "Studded Leather Armor T8",
                "Ring of Vitality T1"
            ],
            "maxedStats": 0,
            "killedBy": "DS Gulpord the Slime God",
            "hadBackpack": false
        }
    ]
}
```
</details>


## Graveyard Summary 
```
graveyardsummary/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- The graveyard summary. 

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "properties": [
        {
            "achievement": "Base fame",
            "total": 69422,
            "max": 5414,
            "average": 68.73,
            "min": 0
        },
        {
            "achievement": "Total fame",
            "total": 122364,
            "max": 20244,
            "average": 121.15,
            "min": 0
        },
        {
            "achievement": "Oryx kills",
            "total": 6,
            "max": 1,
            "average": 0.01,
            "min": 0
        },
        {
            "achievement": "God kills",
            "total": 26379,
            "max": 1083,
            "average": 26.12,
            "min": 0
        },
        {
            "achievement": "Monster kills",
            "total": 325020,
            "max": 10764,
            "average": 321.8,
            "min": 0
        },
        {
            "achievement": "Quests completed",
            "total": 15661,
            "max": 1056,
            "average": 15.51,
            "min": 0
        },
        {
            "achievement": "Tiles uncovered",
            "total": 152371196,
            "max": 3806863,
            "average": 150862.57,
            "min": 2740
        },
        {
            "achievement": "Lost Halls completed",
            "total": 0,
            "max": 0,
            "average": 0,
            "min": 0
        },
        {
            "achievement": "Voids completed",
            "total": 0,
            "max": 0,
            "average": 0,
            "min": 0
        },
        {
            "achievement": "Cultist Hideouts completed",
            "total": 47,
            "max": 15,
            "average": 0.05,
            "min": 0
        }
    ],
    "technicalProperties": [
        {
            "achievement": "God kill assists",
            "total": "91667",
            "max": "1741",
            "average": "90.8",
            "min": "0"
        },
        {
            "achievement": "Monster kill assists",
            "total": "562578",
            "max": "41796",
            "average": "557",
            "min": "0"
        }
    ],
    "statsCharacters": [
        {
            "characterType": "Rogue",
            "stats": [
                143,
                1,
                1,
                0,
                0,
                0,
                0,
                1,
                0
            ],
            "total": 146
        },
        {
            "characterType": "Archer",
            "stats": [
                72,
                2,
                0,
                1,
                0,
                0,
                0,
                1,
                0
            ],
            "total": 76
        },
        {
            "characterType": "Wizard",
            "stats": [
                80,
                3,
                0,
                1,
                0,
                0,
                1,
                0,
                0
            ],
            "total": 85
        }
    ]
}
```
</details>


### Name History Information
```
namehistory/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- The person's name history. 

<details>
<summary>Example (No Name Changes)</summary>
<br>
{"status":0,"nameHistory":[]}
</details>

<details>
<summary>Example (Name Changes)</summary>
<br>
```
{
    "status": 0,
    "nameHistory": [
        {
            "name": "Japan",
            "from": "2017-09-09T09:00:32Z",
            "to": ""
        },
        {
            "name": "Japannnnn",
            "from": "2015-01-13T14:11:43Z",
            "to": "2017-09-09T09:00:32Z"
        },
        {
            "name": "Japannnnn",
            "from": "",
            "to": "2015-01-13T14:11:43Z"
        }
    ]
}
```
</details>


### Guild History Information
```
guildhistory/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- The person's guild history. 

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "guildHistory": [
        {
            "guildName": "Exotics",
            "guildRank": "Founder",
            "from": "2019-01-10T03:10:54Z",
            "to": ""
        },
        {
            "guildName": "Exotics",
            "guildRank": "Leader",
            "from": "2018-12-17T05:02:11Z",
            "to": "2019-01-10T03:10:54Z"
        },
        {
            "guildName": "Exotics",
            "guildRank": "Initiate",
            "from": "2018-11-11T06:13:26Z",
            "to": "2018-12-17T05:02:11Z"
        },
        {
            "guildName": "Not in a guild",
            "guildRank": "",
            "from": "2018-11-11T06:11:44Z",
            "to": "2018-11-11T06:13:26Z"
        },
        {
            "guildName": "Banished Gods",
            "guildRank": "Initiate",
            "from": "2018-02-18T05:55:57Z",
            "to": "2018-11-11T06:11:44Z"
        },
        {
            "guildName": "Not in a guild",
            "guildRank": "",
            "from": "2018-02-17T23:22:32Z",
            "to": "2018-02-18T05:55:57Z"
        },
        {
            "guildName": "Common",
            "guildRank": "Founder",
            "from": "2017-08-09T03:04:31Z",
            "to": "2018-02-17T23:22:32Z"
        }
    ]
}
```
</details>
</details>


### Rank History Information
```
rankhistory/{name}
```

**Parameter(s)**
- name (string): The person to get information for. 

**Return(s)**
- The person's rank history. 

<details>
<summary>Example</summary>
<br>
```
{
    "status": 0,
    "rankHistory": [
        {
            "rank": 71,
            "achieved": "2020-07-31 22:13:49 in ~ 20 hours 17 minutes",
            "date": "2020-07-31T22:13:49Z"
        },
        {
            "rank": 70,
            "achieved": "2020-07-31 01:56:26 in ~ 1 day 23 hours 59 minutes",
            "date": "2020-07-31T01:56:26Z"
        },
        {
            "rank": 69,
            "achieved": "2020-07-29 01:57:15 in ~ 4 days 3 hours 25 minutes",
            "date": "2020-07-29T01:57:15Z"
        }
    ]
}
```
</details>

## Setup Guide
This is coming soon!

## Support the Project
The best way to support this project is to star (⭐) it. You might also consider looking through the code and seeing if something could use optimizing (most of the code is written in the middle of the night). 

## License
Please review the license file in this repository.