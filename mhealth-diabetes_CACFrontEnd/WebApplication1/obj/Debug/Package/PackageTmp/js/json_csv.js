//http://jsfiddle.net/JXrwM/7285/
function Convertor(e) {
    var data = $('#json_txt').val();
    if (data) {
        mostrarMensaje("Un momento por favor. Se está generando el archivo", 5000);
        JSONToCSVConvertor(data, new Date().getFullYear() + "_" + (new Date().getMonth() + 1) + "_" + new Date().getDate(), true);
    } else {
        mostrarMensaje("No se encontró información para descargar.", 5000);
    }
}

function ToDownload(jsonString) {
    $('#json_txt').val(jsonString);
}

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {
            if (no_data.includes(index) === false) {
                //Now convert each value to string and comma-seprated
                row += index + ',';
            }
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            if (no_data.includes(index) === false) {
                row += '"' + arrData[i][index] + '",';
            }
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "Report_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

var no_data = ["Id", "IdArchivo", "IdUsuario"];

function ConvertorSublist(e) {
    var data = $('#json_txt').val();
    if (data) {
        mostrarMensaje("Un momento por favor. Se está generando el archivo", 5000);
        var reportTitle = new Date().getFullYear() + "_" + (new Date().getMonth() + 1) + "_" + new Date().getDate();
        var CSV = GetBody(data, reportTitle, true);
        Download(CSV, reportTitle);
    } else {
        mostrarMensaje("No se encontró información para descargar.", 5000);
    }
}

function GetBody(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
    var CSV = '';
    //Set Report title in first row or line
    CSV += ReportTitle + '\r\n\n';
    //This condition will generate the Label/Header
    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {

        var row = "";
        var array_sublist = [3];
        var index_sublist = 0;
        CSV += 'Item ' + (i + 1) + '\r\n';
        if (ShowLabel) {
            CSV += GetHeader(JSONData) + '\r';
        }

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            if (no_data.includes(index) === false) {
                if (Array.isArray(arrData[i][index]) === false) {
                    row += '"' + arrData[i][index] + '",';
                } else {
                    array_sublist[index_sublist] = Sublist(arrData[i][index], index, true);
                    index_sublist++;
                }
            }
        }
        row += '\r\n';
        if (array_sublist[0].length) {
            row += array_sublist[0];
        }
        if (array_sublist[1].length) {
            row += array_sublist[1];
        }


        row.slice(0, row.length - 1);
        //add a line break after each row
        CSV += row + '\r\n\n';

    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }
    return CSV;
}
 
function GetHeader(JSONData) {
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
    var CSV = '';
    var row = "";
    //This loop will extract the label from 1st index of on array
    for (var index in arrData[0]) {
        if (Array.isArray(arrData[0][index]) === false) {
            if (no_data.includes(index) === false) {
                //Now convert each value to string and comma-seprated
                row += index + ',';
            }
        }
    }
    row = row.slice(0, -1);
    //append Label row with line break
    CSV += row;
    return CSV;
}

function Sublist(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
    var CSV = '';
    //Set Report title in first row or line
    CSV += ReportTitle + '\r';//\n\n';
    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";
        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {
            if (no_data.includes(index) === false) {
                //Now convert each value to string and comma-seprated
                row += index + ',';
            }
        }
        row = row.slice(0, -1);
        //append Label row with line break
        if (row.length > 0) {
            CSV += row + '\r'; //+ '\r\n';
        }

    }
    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";
        for (var index in arrData[i]) {
            if (no_data.includes(index) === false) {
                //2nd loop will extract each column and convert it in string comma-seprated
                row += '"' + arrData[i][index] + '",';
            }
        }
        row.slice(0, row.length - 1);
        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }
    return CSV;
}

function Download(CSV, ReportTitle) {
    //Generate a file name
    var fileName = "Report_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");
    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);
    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    
    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;
    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";
    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}