//
//  Content.swift
//  SwiftUIorg
//
//  Created by Emre Mert on 25.01.2026.
//

import SwiftUI

struct Content: View {

    @FocusState private var amountIsFocused: Bool

    @State private var checkAmount = 0.0
    @State private var numberOfPeople = 2
    @State private var tipPercentage = 20

    private var currencyType: FloatingPointFormatStyle<Double>.Currency {
        .init(code: Locale.current.currency?.identifier ?? "USD")
    }

    // MARK: - Calculations

    var totalTip: Double {
        checkAmount * Double(tipPercentage) / 100
    }

    var grandTotal: Double {
        checkAmount + totalTip
    }

    var totalPerPerson: Double {
        grandTotal / Double(numberOfPeople)
    }

    // MARK: - UI

    var body: some View {
        NavigationStack {
            Form {

                // Amount & People
                Section {
                    TextField("Amount", value: $checkAmount, format: currencyType)
                        .keyboardType(.decimalPad)
                        .focused($amountIsFocused)

                    Picker("Number of people", selection: $numberOfPeople) {
                        ForEach(2..<100) {
                            Text("\($0) people")
                        }
                    }
                }

                // Tip Picker
                Section("How much tip do you want to leave?") {
                    Picker("Tip percentage", selection: $tipPercentage) {
                        ForEach(0..<101) {
                            Text($0, format: .percent)
                        }
                    }
                }

                // Total Amount
                Section("Total Amount") {
                    Text(grandTotal, format: currencyType)
                }

                // Per Person
                Section("Amount per person") {
                    Text(totalPerPerson, format: currencyType)
                        .foregroundColor(tipPercentage == 0 ? .red : .primary)
                }
            }
            .navigationTitle("WeSplit")
            .toolbar {
                ToolbarItemGroup(placement: .keyboard) {
                    Spacer()

                    Button("Done") {
                        amountIsFocused = false
                    }
                }
            }
        }
    }
}

// MARK: - Preview

struct Content_Previews: PreviewProvider {
    static var previews: some View {
        Content()
    }
}

