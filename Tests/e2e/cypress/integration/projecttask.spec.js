/// <reference types="Cypress" />

context('ProjectTask', () => {
    const titleSelector = "#ProjectTask_Title";
    const customFieldTextSelector = "#ProjectTask\\.CustomFields\\[0\\]\\.FieldValue";
    const customFieldCheckboxSelector = "#ProjectTask\\.CustomFields\\[1\\]\\.FieldValue";
    const customFieldRadioSelector = "#ProjectTask\\.CustomFields\\[2\\]\\.FieldValue";
    const customFieldSelectSelector = "#ProjectTask\\.CustomFields\\[3\\]\\.FieldValue";
    const customFieldTextareaSelector = "#ProjectTask\\.CustomFields\\[4\\]\\.FieldValue";


    it("should create task", () => {
        cy.visit("/ProjectTask/Create");
        cy.get(titleSelector).type("Automated test task");
        cy.get(customFieldTextSelector).type("TestField");
        cy.get(customFieldCheckboxSelector).check();
        cy.get(customFieldRadioSelector + ':nth(1)').check();
        cy.get(customFieldSelectSelector).select(" Select Value8");
        cy.get(customFieldTextareaSelector).type("Automated test textarea text");
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/");
        cy.get('tbody tr:last td:first').contains("Automated test task");
    });

    it("should show existing task values on after creating", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get(titleSelector).should("have.value", "Automated test task");
        cy.get(customFieldTextSelector).should("have.value", "TestField");
        cy.get(customFieldCheckboxSelector).should("be.checked");
        cy.get(customFieldRadioSelector + ':nth(1)').should("be.checked");
        cy.get(customFieldSelectSelector).should("have.value", " Select Value8");
        cy.get(customFieldTextareaSelector).contains("Automated test textarea text");
    });

    it("should edit task", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get(titleSelector).type("2");
        cy.get(customFieldTextSelector).type("2");
        cy.get(customFieldRadioSelector +':nth(2)').check();
        cy.get(customFieldSelectSelector).select(" Select Value9");
        cy.get(customFieldTextareaSelector).type(" edited");
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/");
        cy.get('tbody tr:last td:first').contains("Automated test task");
    });

    it("should show existing task values after edit", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get(titleSelector).should("have.value", "Automated test task2");
        cy.get(customFieldTextSelector).should("have.value", "TestField2");
        cy.get(customFieldCheckboxSelector).should("be.checked");
        cy.get(customFieldRadioSelector + ':nth(2)').should("be.checked");
        cy.get(customFieldSelectSelector).should("have.value", " Select Value9");
        cy.get(customFieldTextareaSelector).should("have.value", "Automated test textarea text edited");
    });
});