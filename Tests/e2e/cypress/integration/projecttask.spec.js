/// <reference types="Cypress" />

context('ProjectTask', () => {
    it("should create task", () => {
        cy.visit("/ProjectTask/Create");
        cy.get("#ProjectTask_Title").type("Automated test task");
        cy.get('input[name="ProjectTask.CustomFields[0].FieldValue"]').type("TestField");
        cy.get('#ProjectTask_CustomFields_2__FieldValue').check();
        cy.get('input[name="ProjectTask.CustomFields[2].FieldValue"]:nth(1)').check();
        cy.get('#ProjectTask_CustomFields_4__FieldValue').select(" Select Value8");
        cy.get('#ProjectTask_CustomFields_6__FieldValue').type("Automated test textarea text");
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/");
        cy.get('tbody tr:last td:first').contains("Automated test task");
    });

    it("should show existing task values on after creating", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get("#ProjectTask_Title").should("have.value", "Automated test task");
        cy.get('input[name="ProjectTask.CustomFields[0].FieldValue"]').should("have.value", "TestField");
        cy.get('#ProjectTask_CustomFields_2__FieldValue').should("be.checked");
        cy.get('input[name="ProjectTask.CustomFields[2].FieldValue"]:nth(1)').should("be.checked");
        cy.get('#ProjectTask_CustomFields_4__FieldValue').should("have.value", " Select Value8");
        cy.get('#ProjectTask_CustomFields_6__FieldValue').contains("Automated test textarea text");
    });

    it("should edit task", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get("#ProjectTask_Title").type("2");
        cy.get('input[name="ProjectTask.CustomFields[0].FieldValue"]').type("2");
        cy.get('input[name="ProjectTask.CustomFields[2].FieldValue"]:nth(2)').check();
        cy.get('#ProjectTask_CustomFields_4__FieldValue').select(" Select Value9");
        cy.get('#ProjectTask_CustomFields_6__FieldValue').type(" edited");
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/");
        cy.get('tbody tr:last td:first').contains("Automated test task");
    });

    it("should show existing task values after edit", () => {
        cy.visit("/");
        cy.get("tbody tr:last td:last > a:first").click();
        cy.get("#ProjectTask_Title").should("have.value", "Automated test task2");
        cy.get('input[name="ProjectTask.CustomFields[0].FieldValue"]').should("have.value", "TestField2");
        cy.get('#ProjectTask_CustomFields_2__FieldValue').should("be.checked");
        cy.get('input[name="ProjectTask.CustomFields[2].FieldValue"]:nth(2)').should("be.checked");
        cy.get('#ProjectTask_CustomFields_4__FieldValue').should("have.value", " Select Value9");
        cy.get('#ProjectTask_CustomFields_6__FieldValue').should("have.value", "Automated test textarea text edited");
    });
});