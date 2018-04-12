"use strict";
$(document).ready(function () {
    const fieldType = "#CustomField_FieldType";
    let selectedValue = $(fieldType).val();

    if (selectedValue != undefined) {
        changeFieldVisibility(selectedValue);
    }

    $(fieldType).change(function () {
        selectedValue = $(fieldType).val();
        changeFieldVisibility(selectedValue);
    });
});

function changeFieldVisibility(selectedValue) {
    $(".hideable").addClass("hidden");
    switch (selectedValue) {
    case "0":
    case "4":
        $("#text-fields").removeClass("hidden");
        break;
    case "1":
    case "3":
        $("#field-values").removeClass("hidden");
        break;
    }
}