//
//  ViewController.swift
//  winapp
//
//  Created by Michael Kennedy on 4/3/15.
//  Copyright (c) 2015 DevelopMentor. All rights reserved.
//

import Cocoa

class ViewController: NSViewController {

    @IBOutlet weak var lbl: NSTextField!
    @IBOutlet weak var textBox: NSTextField!
    
    @IBAction func onClick(sender: AnyObject) {
        println("Works!")
    }
    
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }

    override var representedObject: AnyObject? {
        didSet {
        // Update the view, if already loaded.
        }
    }


}

