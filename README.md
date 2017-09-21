Command line tool for bruteforcing solutions for FFXIV Grand Company Squadron missions

It will bruteforce all combinaisons of characters and training offsets and breaks on a valid solution or the closest one in term of pure stats

 * In-game stats are reffered in this order : Tank, Heal, Damage

 * Add a text file named ```characters.txt``` with a character name and stats on each line separated with commas

Example : 
```
Cecily,26,120,34
Hastaloeya,108,26,46
Yehn Amariyo,98,26,56
Rivienne,26,120,34
Rhylharr,62,36,76
Gota,25,101,50
Alimahus,42,25,109
Samga,24,88,62
```

 * Add a text file named ```offsets.txt``` with a training offset name and stats on each line separated with commas

Example : 
```
None,0,0,0
Physical,200,60,20
Mental,140,120,20
Tactical,140,60,80
Physical/Mental,180,100,0
Physical/Tactical,180,40,60
Tactical/Mental,120,80,60
```
