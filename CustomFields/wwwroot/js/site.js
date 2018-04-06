"use strict";
$(document).ready(function () {
    const fieldType = "#CustomField_FieldType";
    let selectedValue = $(fieldType).val();

    if (selectedValue != undefined) {
        changeFields();
    }

    $(fieldType).change(function () {
        $(".hideable").addClass("hidden");
        selectedValue = $(fieldType).val();
        changeFields();
    });

    function changeFields() {
        switch (selectedValue) {
        case "0":
        case "4":
            $("#text-fields").removeClass("hidden");
            break;
        case "1":
        case "2":
        case "3":
            $("#field-values").removeClass("hidden");
            break;
        }
    }
});