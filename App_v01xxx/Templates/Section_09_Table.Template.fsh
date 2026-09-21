//-------------------------------------------------------------------------------------
//
Instance: {{TABLE_ROW_X}}
InstanceOf: m11-reporting-table
Usage: #example

Description: "Table defining the trial reporting of adverse events etc"
* id = "{{TABLE_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* code = http://hl7.org/fhir/ValueSet/basic-resource-type#adminact

// TODO Work out how to distinguish Event Type and Other Event Type
* extension[reporting].extension[eventType][+].valueCodeableConcept ={{C28784}}
* extension[reporting].extension[eventType][+].valueCodeableConcept = {{C28784}}

* extension[reporting].extension[situationalScope].valueString = {{C218785}}
// TODO these should properly be the trial start date and end
* extension[reporting].extension[reportablePeriodStart].valueDate = {{C218786}}
* extension[reporting].extension[reportablePeriodEnd].valueDate = {{C218787}}
* extension[reporting].extension[reportingTiming].valueString = {{C218788}}
* extension[reporting].extension[reportingMethod].valueString = {{C218789}} 
* extension[reporting].extension[backupReportingMethod].valueString = {{C218790}}