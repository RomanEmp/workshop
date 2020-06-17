
// Получение данных с внешнего сервера
function sendAjaxOld() {

    let urlOld = "http://my-json-server.typicode.com/RomaAnisimov/TestJson/items";
    $.get(urlOld, handleDataOld);

}

function sendAjaxNew() {

    let urlNew = "http://my-json-server.typicode.com/RomaAnisimov/ChangedJson/items";
    $.get(urlNew, handleDataNew);

}

//////////////////////////////////////


function handleButtonClick() {  // click button event

    sendAjaxNew();

}


function documentLoaded() { // Загрузить 1-й JSON
    sendAjaxOld();
}




function handleDataOld(data) { // Принять данные с первого JSON, построить и нарисовать дерево


    let tree = createTree(data);

    drowTree(tree, $('#container').get(0));


}



function handleDataNew(data) { // Принять данные со второго JSON , проанализировать и изменить текущее  дерево
    analyseNewJson(data);
    changeTree(data);

}


//  Построение и отрисовка дерева
function createTree(data) {
    let tree = [];
    let temp = [];
    data.forEach(item => {
        temp[item.id] = item;
        temp[item.id].children = [];
    })
    temp.forEach(item => {
        if (item.parentId) {
            temp[item.parentId].children.push(item);
        }
        else {
            tree.push(item);
        }
    })
    return tree;

}
function drowTree(data, root) {
    let ul = $(document.createElement('ul'));

    ul.appendTo(root);
    data.forEach(item => {
        let li = $(document.createElement('li'));
        li.id = item.id;
        li.title = item.title;
        li.className = 'li';
        let span = document.createElement('span'); // let span = $(document.createElement('span'));
        span.id = item.id;
        span.title = item.title;
        span.className = 'span';
        span.innerHTML = "ID: " + item.id + " Title: " + item.title + " Parent ID: " + item.parentId; // span.html("ID: " + item.id + " Title: " + item.title + " Parent ID: " + item.parentId);
        li.append(span); // span.appendTo(li);
        if (item.children) {
            drowTree(item.children, li);
        }
        li.appendTo(ul);
        debugger;
    });

}

////////////////////////////////

//  Обработка данных со второго JSON и изменение дерева
function analyseNewJson(data) {
    let arr = data;
    findID(data);
    findElements(data);
    newTextForEl(data);

}
function findID(data) { // Поиск лишнего ID

    let jsonID = [];
    let treeIDlist = [];

    $("span[ID]").each(function () {
        treeIDlist.push((+$(this)[0].id));
        return treeIDlist;
    });

    data.forEach(item => {
        jsonID.push(+item.id);
        return jsonID;

    });
    let targetID = treeIDlist.filter(e => ! ~jsonID.indexOf(e));
    return (Number(targetID) - 1);
}

function findElements(data) { // Поиск отличающихся по item.title элементов 
    let titlesFromTree = [];
    let titlesFromJson = [];
    let indexlist = [];
    $('span').each(function (i, elem) {

        titlesFromTree.push($(elem).attr('Title'));
        return titlesFromTree;
    });

    data.forEach(item => {
        titlesFromJson.push(item.title);
        return titlesFromJson;
        debugger;
    });

    let targetTitle = titlesFromTree.filter(e => !~titlesFromJson.indexOf(e));
    titlesFromTree.forEach(item => {
        for (i = 0; i <= targetTitle.length; i++) {
            if (item == targetTitle[i]) {
                let indexTitle = titlesFromTree.indexOf(item);
                indexlist.push(indexTitle);
                return indexlist;
            }
        }
    });

    return (indexlist);
}


function newTextForEl(data) { // Содержимое строк на замену
    let TextFromTree = [];
    let TextFromJSON = [];
    $('span').each(function (i, elem) {

        TextFromTree.push($(elem).text());
        return TextFromTree;
    });

    data.forEach(item => {
        TextFromJSON.push("ID: " + item.id + " Title: " + item.title + " Parent ID: " + item.parentId);
        return TextFromJSON;
    });
    let newTitle = TextFromJSON.filter(e => !~TextFromTree.indexOf(e));

    return newTitle.reverse();
}


function changeTree(data) { // Изменение дерева
    let elementTochange = findElements(data);
    let newText = newTextForEl(data);
    let elementToDelete = findID(data);
    $('span').each(function (i, elem) {
        for (let j = 0; j <= elementTochange.length; j++) {

            if (i == elementTochange[j]) {
                $(elem).text(newText[j] + '(Было:' + $(elem).text() + ')');
                $(elem).css({ 'color': 'green', 'cursor': 'pointer', 'font-size': '14px' });

            }
        }
    });

    $('li').each(function (i, elem) {
        if (i == elementToDelete) {
            $(elem).remove();
        }
    });
}
/////////////////////////////////////////////////////


// Определяем состояние готовности DOM
$(document).ready(documentLoaded);

$('#run_script').one('click', handleButtonClick);

