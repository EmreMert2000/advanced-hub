//
//  First.swift
//  SwiftUIorg
//
//  Created by Emre Mert on 25.01.2026.
//

import SwiftUI

struct FirstSection: View {
    var body: some View {
        NavigationView{
            Form{
                Section {
                    Text("Hello, world!")
                    Text("Hello, world!")
                    Text("Hello, world!")
                }
                
                Section {
                    Text("Hello, world!")
                    Text("Hello, world!")
                }
            }.navigationTitle("Hello Form")
                .navigationBarTitleDisplayMode(.inline)
        }
    }
}

struct FirstSection_Previews: PreviewProvider {
    static var previews: some View {
        FirstSection()
    }
}
