"use strict";

document.getElementById("CustomField_FieldType").addEventListener('change', function (event) {
    changeFieldVisibility(event.target.value);
});

function changeFieldVisibility(selectedValue) {
    if (selectedValueIsOfTextFieldType(selectedValue)) {
        document.getElementById("text-fields").classList.remove("hidden");
        document.getElementById("field-values").classList.add("hidden");
    } else if (selectedValueIsOfSelectionFieldType(selectedValue)) {
        document.getElementById("field-values").classList.remove("hidden");
        document.getElementById("text-fields").classList.add("hidden");
    }
}

function selectedValueIsOfTextFieldType(selectedValue) {
    return selectedValue === "0" || selectedValue === "4";
}

function selectedValueIsOfSelectionFieldType(selectedValue) {
    return selectedValue === "1" || selectedValue === "3";
}