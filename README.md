# LBS RPG [1]

## Preview
![](https://lh3.googleusercontent.com/AT-5ibo3WkA6H1i2uWEqUlBgtEVjb8JC8F0FHSPj8mSgTkMmAgV5YduASDjxzftWfY-pQ5vqkD-WAzDkwJsGIm6Nkf41TOYmrVKkoUGp_czdXgREyX5x3Z0Ipx8ptMxwJKceBdI6)

![](https://lh4.googleusercontent.com/cerzlZgCd14g3nsESOBBhcYBQVkmShcaKq0B4xwsvU0zkE5bdk8tiv9RSl7PBB1J5URaLsWgzKuLYhun3mS5Kv_-F7zszSKMkmjASiVrrjYfKgZTFnyv4UjTxEHT_I3uZ2tancHA)

## Author Notes [SV]
Spelet handlar om en kille som förlorat människors förtroende. Hans mål är att bli respekterad åtminstone i en by. Spelaren kan göra många saker, som att resa till andra byar, köpa grejer och bråka med monster. Världen som spelaren befinner sig i blev invaderad av en okänd ras, och därför är det extra viktigt att kunna skydda sig själv. Spelaren får respekt genom att skydda människor och köpa grejer i affären. Ju fler respektpoäng spelaren har, desto mindre pris ska han vara tvungen betala i affären. Spelet är strukturerat i olika sektioner, som affären, huset och fängelsehålan. I alla de sektionerna finns olika aktiviteter som kan hjälpa spelaren att få pengar och respektpoäng.

Koden är strikt-strukturerad. D.v.s. varje fil/class tillhör sin egen sektion/mapp. Jag använde  polymorfism, avancerade objekt, fileio och många andra grejer i det här projektet. Alla instances implementerar deras egna interfaces, därför är spelets struktur lätt att följa och utveckla. Själva projektet utvecklades med förenklad CI-metod. Den metoden är inte så viktig att ha när man utvecklar spel ensam, men det var viktigt för mig eftersom jag försökte eliminera buggar med hjälp av unit tests. Varje repository branch (feature) har runt 20 unit-tester, som validerade att koden fungerar som den ska.

Jag tror att jag lyckades skapa en intressant player experience. Medan de andra konsolspelen använder text input behaviour för att låta spelaren kontrollera hjälten, mitt spel använder vanliga hot-keys. Det funkar på grund av bra detaljerade classes som jag återanvänder i min kod. Controls är intuitiva och det är lätt att börja och utveckla sin hjälte. Eftersom det är ett RPG spel kan man utveckla stats genom att köpa nya grejer. Vissa saker är väldigt dyra, och därför kan det ta tid att få dem.

UI-komponenterna är mycket intressanta och skapar en fantastisk UX. Trots att det är ett konsolspel lyckades jag skapa en enkel 2D-motor för fängelsehålorna. Monstren kan röra sig och attackera spelaren. Spelaren kan i sin tur attackera dem med vapen och röra sig med arrow keys. 

Det finns många grejer som man kan lägga till och det är lätt att göra då alla komponenter enkelt kan utvidgas. Detta är möjligt för att jag följde SOLID principles under utvecklingstiden. Jag tycker att koden som jag lämnade in innehåller basen för ett rikt RPG 
-spel som kan vara intressant till och med för människor som gillar grafiska spel.

Som sagt, UI och UX är bra. Det är mina favoritdelar av spelet för det bevisar att man kan skapa vackra saker även med dåliga instrument.

## Credits
Oles Odynets
September 2020
15 Hours of Development
