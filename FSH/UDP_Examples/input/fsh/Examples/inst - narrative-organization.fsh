//=======================================
//
Instance: Narrative-Organization
InstanceOf: Organization
Title: "Exemplar Organization as Narrative Author"
Usage: #example
Description: "A minimal Organization definition"

//* id = "narrative-organization" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* name = "Exemplar Pharmaceuticals Co."
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "Exemplar Road"
* contact[=].address.city = "Exemplarton"
* contact[=].address.postalCode = "EX99 2AB"
* contact[=].address.country = "United Kingdom"

* contact[+].purpose.coding[+] = $NCIT#C70946 "Postal Address"
* contact[=].address.line[+] = "Box 123"
* contact[=].address.city = "Exemplarton"
* contact[=].address.country = "United Kingdom"
