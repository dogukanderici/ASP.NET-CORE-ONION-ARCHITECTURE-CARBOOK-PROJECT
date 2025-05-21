let currentPageNumber = parseInt($("#CurrentPageNumber").text());
let totalPageNumber = parseInt($("#TotalPageNumber").text());

if (currentPageNumber == 1) {
    $("#PreviousPage").addClass("disabled");
}

if (currentPageNumber == totalPageNumber) {
    $("#NextPage").addClass("disabled");
}

$(document).ready(function () {

    // Önceki ve Sonraki Buton Kodları
    $(document).on("click", "#PreviousPage", function () {

        let tempCurrentPage = parseInt($("#CurrentPageNumber").text());
        $("#CurrentPageNumber").text(tempCurrentPage - 1);

        if (tempCurrentPage == 1) {
            $("#CurrentPageNumber").text(1);
        }

        adjustPaging();
    });

    $(document).on("click", "#NextPage", function () {

        let tempCurrentPage = parseInt($("#CurrentPageNumber").text());
        $("#CurrentPageNumber").text(tempCurrentPage + 1);

        if (tempCurrentPage == totalPageNumber) {
            $("#CurrentPageNumber").text(totalPageNumber);
        }

        adjustPaging();
    });

    // Sayfa Sayılarına Göre Buton Kodu
    $(document).on("click", ".customPaging", function () {
        $("#CurrentPageNumber").text($(this).text());
        adjustPaging();
    });

    // Css Düzenlemeleri
    $(document).on("click", "#TablePagination", function () {

        let tempCurrentPage = parseInt($("#CurrentPageNumber").text());

        if (tempCurrentPage == 1) {
            $("#PreviousPage").addClass("disabled");
        }
        else {
            $("#PreviousPage").removeClass("disabled");
        }

        if (tempCurrentPage == totalPageNumber) {
            $("#NextPage").addClass("disabled");
        }
        else {
            $("#NextPage").removeClass("disabled");
        }

        $(".customPaging").filter(function () {
            return $(this).text().trim() == tempCurrentPage;
        }).addClass("btn btn-primary get-page-data");

        $(".customPaging").filter(function () {
            return $(this).text().trim() != tempCurrentPage;
        }).removeClass("btn btn-primary");
    });
});

function adjustPaging() {

    let currentPageNumber = parseInt($("#CurrentPageNumber").text());
    let totalPageNumber = parseInt($("#TotalPageNumber").text());

    let tempPagingHtmlStringData = '<li class="page-item"><button class="page-link customPaging get-page-data">1</button></li>';

    if (totalPageNumber > 4) {
        if (currentPageNumber == 1) {
            tempPagingHtmlStringData += '<li class="page-item"><button class="page-link customPaging get-page-data">2</button></li>' +
                '<li class="page-item disabled"><button class="page-link">...</button></li>';
        }
        else if (currentPageNumber == totalPageNumber) {
            tempPagingHtmlStringData += '<li class="page-item disabled"><button class="page-link">...</button></li>' +
                '<li class="page-item"><button class="page-link customPaging get-page-data">' + (totalPageNumber - 1) + '</button></li>';
        }
        else {
            if (currentPageNumber == 2) {
                tempPagingHtmlStringData += '<li class="page-item"><button class="page-link customPaging get-page-data">' + currentPageNumber + '</button></li>' +
                    '<li class="page-item"><button class="page-link customPaging get-page-data">' + (currentPageNumber + 1) + '</button></li>' +
                    '<li class="page-item disabled"><button class="page-link">...</button></li>';
            }
            else if (currentPageNumber == (totalPageNumber - 1)) {
                tempPagingHtmlStringData += '<li class="page-item disabled"><button class="page-link">...</button></li>' +
                    '<li class="page-item"><button class="page-link customPaging get-page-data">' + (currentPageNumber - 1) + '</button></li>' +
                    '<li class="page-item"><button class="page-link customPaging get-page-data">' + currentPageNumber + '</button></li>';
            }
            else {
                if (currentPageNumber != 3)
                    tempPagingHtmlStringData += '<li class="page-item disabled"><button class="page-link">...</button></li>';

                tempPagingHtmlStringData += '<li class="page-item"><button class="page-link customPaging get-page-data">' + (currentPageNumber - 1) + '</button></li>' +
                    '<li class="page-item"><button class="page-link customPaging get-page-data">' + (currentPageNumber) + '</button></li>' +
                    '<li class="page-item"><button class="page-link customPaging get-page-data">' + (currentPageNumber + 1) + '</button></li>';

                if ((currentPageNumber + 2) != totalPageNumber)
                    tempPagingHtmlStringData += '<li class="page-item disabled"><button class="page-link">...</button></li>';
            }
        }

        tempPagingHtmlStringData += '<li class="page-item"><button class="page-link customPaging get-page-data">' + totalPageNumber + '</button></li>';
    }
    else {
        for (let i = 2; i <= totalPageNumber; ++i) {
            tempPagingHtmlStringData += '<li class="page-item"><button class="page-link customPaging get-page-data">' + i + '</button></li>';
        }
    }

    $(".pagination").empty();
    let dataString = '<li id="PreviousPage" class="page-item">' +
        '<button class="page-link previous-next-page get-page-data">Önceki</button>' +
        '</li>' +

        tempPagingHtmlStringData +

        '<li id="NextPage" class="page-item">' +
        '<button class="page-link previous-next-page get-page-data">Sonraki</button>' +
        '</li>';

    $(".pagination").html(dataString);
}