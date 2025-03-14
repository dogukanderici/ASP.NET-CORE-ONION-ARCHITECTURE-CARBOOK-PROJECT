// Türkçe dil desteği
$.fn.datepicker.dates['tr'] = {
    days: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
    daysShort: ["Paz", "Pz", "Sal", "Çar", "Per", "Cum", "Cmt"],
    daysMin: ["Pz", "Pt", "Sal", "Ça", "Pe", "Cu", "Ct"],
    months: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
    monthsShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
    today: "Bugün",
    clear: "Temizle",
    weekStart: 1,
    format: "dd.mm.yyyy"
};

$('#baslangic_tarih, #bitis_tarih').datepicker({
    'format': 'dd.mm.yyyy',
    'startDate': new Date(),
    'autoclose': true,
    'todayHighlight': true,
    'language': 'tr'
});

var now = new Date();
var hours = now.getHours();
var minutes = now.getMinutes();

if (minutes < 30) {
    minutes = 30;
}
else {
    minutes = 60;
}

$('#baslangic_saat').timepicker({
    timeFormat: 'H:i',    // 24 saat formatı
    step: 30,             // 30 dakikalık adımlar (örn: 12:00, 12:30, 13:00)
    minTime: new Date(now.setMinutes(minutes)).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }).toString(),     // Minimum saat (Anlık saat değerine göre ayarlanmış)
    maxTime: '23:30',     // Maksimum saat (Gece 23:30)
    dynamic: false,
    dropdown: true,       // Seçim için açılır liste
    scrollbar: true       // Uzun listelerde scrollbar göster
});

$('#bitis_saat').timepicker({
    timeFormat: 'H:i',    // 24 saat formatı
    step: 30,             // 30 dakikalık adımlar (örn: 12:00, 12:30, 13:00)
    minTime: '00:00',     // Minimum saat (Gece yarısı)
    maxTime: '23:30',     // Maksimum saat (Gece 23:30)
    dynamic: false,
    dropdown: true,       // Seçim için açılır liste
    scrollbar: true       // Uzun listelerde scrollbar göster
});