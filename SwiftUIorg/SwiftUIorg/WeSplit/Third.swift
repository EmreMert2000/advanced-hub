//
//  Third.swift
//  SwiftUIorg
//
//  Created by Emre Mert on 25.01.2026.
//


import SwiftUI

struct ThirdSection: View {
    let students = ["Emre", "Mert", "Derya"]
    @State private var selectedStudent = "Emre"
    
    var body: some View {
        NavigationView {
            Form {
                Picker("Select your student", selection: $selectedStudent) {
                    ForEach(students, id: \.self) {
                        Text($0)
                    }
                }
            }
        }
    }
}

struct ThirdSection_Previews: PreviewProvider {
    static var previews: some View {
        ThirdSection()
    }
}
