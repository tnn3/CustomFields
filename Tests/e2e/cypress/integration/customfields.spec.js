/// <reference types="Cypress" />

context('CustomFields', () => {
    it("should create custom field", () => {
        cy.visit("/CustomField/Create");
        cy.get("#CustomField_FieldName_FieldDefaultName").type("Field test");
        cy.get("#CustomField_FieldType").select("Text");
        cy.get('input[name="CustomField.Status"]').check();
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/CustomField");
        cy.get('tbody tr:last td:first').contains("Field test");
    });

    it("should edit custom field", () => {
        cy.visit("/CustomField");
        cy.get('tbody tr:last .btn-warning').click();
        cy.get("#CustomField_FieldName_FieldDefaultName").clear().type("Field test edited");
        cy.get('input[type="submit"]').click();
        cy.url().should('eq', Cypress.config().baseUrl + "/CustomField");
        cy.get('tbody tr:last td:first').contains("Field test edited");
    });
});