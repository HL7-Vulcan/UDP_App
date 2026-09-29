//--------------------------------------------------------------------------------------
//
Profile: M11-objective-table
Parent: Basic
Id: m11-objective-table
Description: """Profile of Basic resource to be used as Reporting details  according to M11 Table of reportings """
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  m11-objective named objective 0..* MS 


//--------------------------------------------------------------------------------------
//
Extension: M11Objective
Id:  m11-objective
Title:  "M11 Section 03.01.01, 03.02.01 and 03.03.01"
Description: """Fixed structure for use in Section 03 Objectives

Objective[]
    Estimand[]
        population
        treatment[]
            usdm
        frequency
        route
    ICE[]
        event
        strategy

"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  section 1..1 MS and  // must be one of C218528 Primary Objective, C218530 Secondary Objective, C218532 Exploratory Objective
  m11-estimands named estimands 1..* MS and
  m11-intercurrent-events named intercurrentEvents 0..*

* extension[section].value[x] only CodeableConcept
* extension[section].value[x] from udp-section-codes-03-vs

/* * extension[objective].value[x] only string
  * ^short = "Objective"
  * ^definition = "C85826,C85827 or C163559 - an objective of the trial."
  * ^comment = "The minimum requirement is one Primary objective, but there can be multiples.  Each objective has an associted set of estimands and intercurrent events." */



//--------------------------------------------------------------------------------------
//
Extension: M11Estimands
Id:  m11-estimands
Title:  "M11 Section 03 - Estimands Table"
Description: """Fixed structure for use in Section 03

Estimand[]
    population
    treatment[]
        usdm
    frequency
    route

"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  population 0..1 MS and
  treatment 0..* MS and
  endpoint 1..1 MS and
  summary 0..1

* extension[population].value[x] only string

* extension[treatment].value[x] only string or Reference(usdm-estimand-treatment)

* extension[endpoint].value[x] only string

* extension[summary].value[x] only string



//--------------------------------------------------------------------------------------
//
Extension: USDMEstimandTreatment
Id:  usdm-estimand-treatment
Title:  "M11 Section 03 - USDM Treatment"
Description: """Fixed structure for USDM representation of administerable product in Estimand table.

treatment
usdm-product
usdm-quantity
usdm-frequency
usdm-route

"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active
  
* extension contains
  treatment 0..1 MS and
  usdm-product 1..1 MS and
  usdm-quantity 1..1 MS and
  usdm-frequency 1..1 MS and
  usdm-route 1..1 MS

* extension[treatment].value[x] only string

/// TODO the usdm-estimand-treatment values should be more than string
* extension[usdm-product].value[x] only string
* extension[usdm-quantity].value[x] only string
* extension[usdm-frequency].value[x] only string
* extension[usdm-route].value[x] only string


//--------------------------------------------------------------------------------------
//
Extension: M11IntercurrentEvents
Id:   m11-intercurrent-events
Title:  "M11 Section 03 - Intercurrent Events Table Row"
Description: """Intercurrent Event and associated Strategy 

ICE
    event
    strategy


"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active
  
* extension contains
  event 1..1 MS and
  strategy 1..1 MS

* extension[event].value[x] only string
* extension[strategy].value[x] only string