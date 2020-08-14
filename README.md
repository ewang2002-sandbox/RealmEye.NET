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

## Project Progress
### Profile Page
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

### Guild Page
- [ ] Main Page
- [ ] Members List
- [ ] Top Characters
- [ ] Top Pets
- [ ] Recent Deaths
- [ ] Fame History
- [ ] Former Members
- [ ] Server Activity

### Mystery Boxes
- [ ] Main Page

### Trading
- [ ] Item to ID & ID to Item Conversion
- [ ] Offers Page

## Setup Guide
This is coming soon!

## Support the Project
The best way to support this project is to star (⭐) it. You might also consider looking through the code and seeing if something could use optimizing (most of the code is written in the middle of the night).

## License
Please review the license file in this repository.