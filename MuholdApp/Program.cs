using MuholdApp;
List<Muhold> muholdak = new List<Muhold>();
int hibasSorokSzama = 0;

foreach (var item in File.ReadAllLines("muholdak.txt").Skip(1))
{
    string[] adatok = item.Split("|");
    string tipus = adatok[0];
    string azon = adatok[1];
    int suly;
    int muszer;


    if(adatok.Count() != 4)
    {
        hibasSorokSzama++;
        continue;
    }
    if (string.IsNullOrWhiteSpace(azon))
    {
        hibasSorokSzama++;
        continue;
    }
    if (tipus != "M" && tipus != "K")
    {
        hibasSorokSzama++;
        continue;
    }
    if (!(int.TryParse(adatok[2],out suly)) || !(int.TryParse(adatok[3],out muszer)))
    {
        hibasSorokSzama++;
        continue;
    }

    if(tipus == "M")
    {
        Muhold muhold = new Muhold(azon,suly);
        muholdak.Add(muhold);
    }
    else
    {
        KutatasiMuhold kutat = new KutatasiMuhold(azon, suly, muszer);
        muholdak.Add(kutat);
    }
}

var kimenet = muholdak.Where(mu => mu.NapiKoltseg() > 2000).OrderByDescending(mu => mu.NapiKoltseg()).Select(mu => $"{mu.Azonosito};{mu.SulyKg};{mu.NapiKoltseg()}");
File.WriteAllLines("riport.txt", kimenet);




Console.WriteLine($"Sikeresen beolvasott műholdak: {muholdak.Count} db");
Console.WriteLine($"Kiszűrt hibás sorok száma: {hibasSorokSzama} db");
Console.WriteLine();
Console.WriteLine("Beolvasott műholdak listája:");
foreach (var item in muholdak)
{
    Console.WriteLine(item);
}
Console.WriteLine();
Console.WriteLine("LINQ Statisztikák:");
Console.WriteLine($"Műholdak átlagos tömege: {muholdak.Average(mu => mu.SulyKg)} kg");
Console.WriteLine($"Legnehezebb műhold: {muholdak.MaxBy(mu => mu.SulyKg).Azonosito} ({muholdak.MaxBy(mu => mu.SulyKg).SulyKg} kg)");
Console.WriteLine($"Legalacsonyabb napi költségű műhold: {muholdak.MinBy(mu => mu.NapiKoltseg()).Azonosito} ({muholdak.MinBy(mu => mu.NapiKoltseg()).NapiKoltseg()} EUR)");
Console.WriteLine($"Legalább 1000 kg-os műholdak száma: {muholdak.Where(mu => mu.SulyKg >= 1000).Count()} db");
Console.WriteLine();
Console.WriteLine("Kutatási műholdak külön értelmezése:");
foreach (var item in muholdak)
{
    if(item is KutatasiMuhold km)
    {
        Console.WriteLine($"- {km.Azonosito} kutatási máhold műszereinek száma: {km.MuszerekSzama} db.");
    }
}
Console.WriteLine();
Console.WriteLine("A riport.txt fájl sikeresen elkészült a kimeneti mappában.");
Console.WriteLine($"Műhold példányok száma (static): {Muhold.OsszesMuholdSzama} db");