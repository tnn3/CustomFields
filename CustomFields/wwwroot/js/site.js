"use strict";
$(document).ready(function () {
    $("#FieldType").change(function () {
        $(".hideable").addClass("hidden");
        const selectedValue = $("#FieldType").val();
        switch (selectedValue) {
            case "0":
            case "6":
                $("#field-lengths").removeClass("hidden");
                break;
            case "1":
            case "2":
            case "3":
                $("#field-values").removeClass("hidden");
                break;
        }
    });
});