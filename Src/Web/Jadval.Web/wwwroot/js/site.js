﻿var selectedLabel = null;
function Load(model) {
    const CreateAndAppend = (name, element, cls = null, text = null) => {
        var temp = document.createElement(name);
        if (cls) temp.classList = cls;
        if (text) temp.innerText = text;
        element.appendChild(temp);
        return temp;
    };
    const IsNumber = (char) => {
        if (char) return "123456789".includes(char);
        return false;
    };
    const CreateQuestionPosition = (element, pos) => {
        var flash = CreateAndAppend("img", element);
        if (pos.startsWith("left"))
            flash.classList = "sam_jadval_table_td_div_box_question_flash_left";

        if (pos.startsWith("right"))
            flash.classList = "sam_jadval_table_td_div_box_question_flash_right";

        if (pos.startsWith("bottom"))
            flash.classList = "sam_jadval_table_td_div_box_question_flash_bottom";

        if (pos.startsWith("top"))
            flash.classList = "sam_jadval_table_td_div_box_question_flash_top";

        flash.setAttribute("src", `./images/flashs/${pos}.png`);
    };

    var table = CreateAndAppend("table", document.getElementById("divBody"), "sam_jadval_table");
    const CheckModel = (newModel) => {
        var wwww = [];
        newModel.forEach((p) => {
            wwww.push({
                row: p.row,
                col: p.col,
                value: document.querySelector(
                    `.sam_jadval_table_td_div_box_center[row='${p.row}'][col='${p.col}']`
                ).innerText,
            });
        });

        fetch("/Jadval/Check", {
            method: "POST",
            body: JSON.stringify({ items: wwww }),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            },
        })
            .then((response) => response.json())
            .then((json) => {
                json.forEach((element) => {
                    if (element.result) {
                        var ee = document.querySelector(
                            `.sam_jadval_table_td_div_box_center[row='${element.row}'][col='${element.col}']`
                        );
                        ee.parentElement.style.backgroundColor = "white";
                    }
                });
            });

        return { one: false, two: true };
    };

    var rowIndexer = 0;
    var modelCheckAll = [];
    model.data.forEach((row) => {
        var tr = CreateAndAppend("tr", table);
        var colIndexer = 0;
        row.forEach((col) => {
            var td = CreateAndAppend("td", tr);
            if (col == "") {
            } else {
                if (IsNumber(col)) {
                    var temp = CreateAndAppend(
                        "div",
                        td,
                        "sam_jadval_table_td_div_box sam_jadval_table_td_div_box_questions"
                    );

                    var questions = model.questions.filter((p) => p.id == Number(col))[0]
                        .value;
                    questions.forEach((item) => {
                        var questionBox = CreateAndAppend(
                            "div",
                            temp,
                            "sam_jadval_table_td_div_box_question"
                        );
                        questionBox.style.height = 100 / questions.length + "%";
                        var jj = CreateAndAppend(
                            "div",
                            questionBox,
                            "sam_jadval_table_td_div_box_center",
                            item.question
                        );
                        if (item != questions[questions.length - 1])
                            CreateAndAppend("hr", temp);

                        CreateQuestionPosition(questionBox, item.position);
                    });
                } else {
                    var dibvLable = CreateAndAppend(
                        "div",
                        td,
                        "sam_jadval_table_td_div_box sam_jadval_table_td_div_box_char"
                    );
                    var lable = CreateAndAppend(
                        "div",
                        dibvLable,
                        "sam_jadval_table_td_div_box_center",
                        col
                    );
                    lable.setAttribute("row", rowIndexer);
                    lable.setAttribute("col", colIndexer);
                    modelCheckAll.push({ row: rowIndexer, col: colIndexer });
                    dibvLable.onclick = function () {
                        if (dibvLable.style.backgroundColor != "white") {
                            if (selectedLabel == null) {
                                selectedLabel = lable;
                                selectedLabel.parentElement.style.backgroundColor = "darkgrey";
                            } else {
                                var ee = lable.innerText;
                                lable.innerText = selectedLabel.innerText;
                                selectedLabel.innerText = ee;
                                selectedLabel.parentElement.style.backgroundColor = "";
                                CheckModel([
                                    {
                                        row: lable.attributes["row"].value,
                                        col: lable.attributes["col"].value,
                                    },
                                    {
                                        row: selectedLabel.attributes["row"].value,
                                        col: selectedLabel.attributes["col"].value,
                                    },
                                ]);
                                selectedLabel = null;
                            }
                        }
                    };
                }
            }
            colIndexer++;
        });
        rowIndexer++;
    });
    CheckModel(modelCheckAll);
}