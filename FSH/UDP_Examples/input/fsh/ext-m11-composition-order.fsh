Extension: M11_CompositionOrder
Id: m11-composition-order
Description: "Addition of an order value to composition sections"
Context: Composition
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^extension[$ext-wg].valueCode = #brr
* ^status = #active

* value[x] only integer or string
* . ^short = "Order number or string"
* . ^definition = "A number or string that can be used to order sections"
