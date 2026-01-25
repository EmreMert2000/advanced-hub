//
//  SwiftUIorgApp.swift
//  SwiftUIorg
//
//  Created by Emre Mert on 25.01.2026.
//

import SwiftUI
import CoreData

@main
struct SwiftUIorgApp: App {
    let persistenceController = PersistenceController.shared

    var body: some Scene {
        WindowGroup {
            ContentView()
                .environment(\.managedObjectContext, persistenceController.container.viewContext)
        }
    }
}
