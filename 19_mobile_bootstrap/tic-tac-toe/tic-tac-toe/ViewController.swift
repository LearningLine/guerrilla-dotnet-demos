//
//  ViewController.swift
//  tic-tac-toe
//
//  Created by Michael Kennedy on 3/3/15.
//  Copyright (c) 2015 DevelopMentor. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    @IBOutlet weak var btn11: UIButton!
    @IBOutlet weak var btn12: UIButton!
    @IBOutlet weak var btn13: UIButton!
    
    @IBOutlet weak var btn21: UIButton!
    @IBOutlet weak var btn22: UIButton!
    @IBOutlet weak var btn23: UIButton!
    
    @IBOutlet weak var btn31: UIButton!
    @IBOutlet weak var btn32: UIButton!
    @IBOutlet weak var btn33: UIButton!
    
    @IBOutlet weak var errorLab: UILabel!
    @IBOutlet weak var winLabel: UILabel!
    var buttonSet : [[UIButton!]] = []
    var game = Game()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        buildButtonSet()
        syncGameText()
        errorLab.text = ""
        winLabel.text = ""
    }
    
    func buildButtonSet() {
        var row1 = [btn11, btn12, btn13]
        var row2 = [btn21, btn22, btn23]
        var row3 = [btn31, btn32, btn33]
        
        buttonSet.append(row1)
        buttonSet.append(row2)
        buttonSet.append(row3)
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    @IBAction func onPlayClick(sender: UIButton) {
        
        if game.hasWinner {
            return
        }
        
        errorLab.text = ""
        winLabel.text = ""
        let (row, col) = getCellIndex(sender)
        if game.play(row, col: col) {
            game.switchTurns()
        }
        else {
            errorLab.text = "ERROR: Cannot play there."
        }
        syncGameText()
        
        if game.hasWinner {
            var player = game.getWinner() == game.x ? "X" : "O"
            winLabel.text = player + " has won!"
        }
    }
    
    func getCellIndex(targetBtn : UIButton!) -> (Int, Int) {
        for (r_idx, row) in enumerate(buttonSet) {
            for (c_idx, btn) in enumerate(row) {
                if targetBtn == btn {
                    return (r_idx, c_idx)
                }
            }
        }
        return (-1, -1)
    }

    func syncGameText() {
        for (r_idx, row) in enumerate(game.board) {
            for (c_idx, cell) in enumerate(row) {
                var txt = getCellText(cell)
                buttonSet[r_idx][c_idx].setTitle(txt, forState: UIControlState.Normal)
            }
        }

    }
    
    func getCellText(cellValue : Int) -> String {
        if cellValue == game.x {
            return "X"
        }
        else if cellValue == game.o {
            return "0"
        }
        return "."
    }
}

