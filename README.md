# RealmEyeNET
An unofficial RealmEye API designed for advanced player verification. 

NOTE: At the moment, this API is in testing and IS expected to break. Do not rely on this API for production use. 

## Purpose
<details>
<summary>Click Here!</summary>
<br>

Currently, there are two major public RealmEye APIs. However, the APIs have either been shut down or is in the process of shutting down. 

- Tiffit's API: Taken down on June 2nd, 2020 as the developer quit Realm and though hosting the API was more of a job than anything else. 
- [Nightfirecat's API](https://github.com/Nightfirecat/RealmEye-API): Pretty outdated and expected to reach end-of-life soon (__apparently__, from the developer himself).

While both APIs have done an amazing job, the developers that have worked on these APIs have sadly moved on to pursue other interests. I have decided, then, to write my own API so I can continue to use it for my [bot](https://github.com/DungeoneerExalt/ZeroRaidBot/). Eventually, I will host the API and release it for the public to use. 

</details>

## Technologies
- .NET Core 3.1
- ASP.NET

## Project Template
The project is divided into three solutions -- two solutions are actively in use. 

- [RealmEyeApi](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeApi): The ASP.NET project that will be used to host the RealmEye API (RealmEyeNET). 
- [RealmEyeNET](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeNET): The .NET Core project containing code that scrapes RealmEye's website. This is the "main" project. 
- [RealmEyeTest](https://github.com/ewang2002/RealmEye.NET/tree/master/RealmEyeTest): A simple Console application designed solely for testing the RealmEyeNET project.

## Project Progress
<details>
<summary>Click Here!</summary>
<br>

#### Profile Page
- [x] Basics
- [x] Character Information 
- [ ] Skins
- [ ] Offers
- [x] Pet Yard
- [x] Graveyard
- [x] Graveyard Summary
- [ ] Fame History
- [x] Rank History
- [x] Name History
- [x] Guild History
- [ ] Pet Graveyard

#### Guild Page
- [ ] Main Page
- [ ] Members List
- [ ] Top Characters
- [ ] Top Pets
- [ ] Recent Deaths
- [ ] Fame History
- [ ] Former Members
- [ ] Server Activity

#### Mystery Boxes
- [ ] Main Page

#### Trading
- [ ] Item to ID & ID to Item Conversion
- [ ] Offers Page

### Production
- [ ] Get Hosting for API
- [ ] Public Release

</details>

## Setup Guide
This is coming soon!

## Support the Project
- The best way to support this project is to star (⭐) it.
- If you have feedback, I would love to hear it! Submit an Issue and I'll look into it as soon as possible.

## License (MIT)
Please review the license file in this repository.