"use strict";

document.getElementById("CustomField_FieldType").addEventListener("change", function (event) {
    changeFieldVisibility(event.target.value);
});

function changeFieldVisibility(selectedValue) {
    document.getElementById("field-values").classList.add("hidden");
    document.getElementById("text-fields").classList.add("hidden");
    if (selectedValueIsOfTextFieldType(selectedValue)) {
        document.getElementById("text-fields").classList.remove("hidden");
    } else if (selectedValueIsOfSelectionFieldType(selectedValue)) {
        document.getElementById("field-values").classList.remove("hidden");
    }
}

function selectedValueIsOfTextFieldType(selectedValue) {
    return selectedValue === "0" || selectedValue === "4";
}

function selectedValueIsOfSelectionFieldType(selectedValue) {
    return selectedValue === "1" || selectedValue === "3";
}