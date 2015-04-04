//
//  Game.swift
//  tic-tac-toe
//
//  Created by Michael Kennedy on 3/3/15.
//  Copyright (c) 2015 DevelopMentor. All rights reserved.
//

import Foundation

class Game {
    
    var board : [[Int]] = [
        [0, 0,  0],
        [0, 0,  0],
        [0, 0,  0]
    ]
    
    var empty = 0
    var x = 1
    var o = -1
    var currentPlayer = 1
    
    init() {
        
    }
    
    func play(row : Int, col : Int) -> Bool {
        var cell = board[row][col]
        if cell != empty {
            return false
        }
        
        board[row][col] = currentPlayer
        return true
    }
    
    func switchTurns() {
        currentPlayer *= -1
    }
    
    internal var hasWinner : Bool {
        get {
            return getWinner() != empty
        }
    }
    
    func getWinner() -> Int {
        var b = board
        var winner = empty
        
        var wins : [[Int]] = [
            [ b[0][0], b[1][1], b[2][2] ], // forward diagonal
            [ b[2][0], b[1][1], b[0][2] ], // reverse diagonal
            [ b[0][0], b[1][0], b[2][0] ], // col 1
            [ b[0][1], b[1][1], b[2][1] ], // col 2
            [ b[0][2], b[1][2], b[2][2] ], // col 3
            [ b[0][0], b[0][1], b[0][2] ], // row 1
            [ b[1][0], b[1][1], b[1][2] ], // row 2
            [ b[2][0], b[2][1], b[2][2] ], // row 3
        ]
        
        for win in wins {
            var sum : Int = win.reduce(0, combine: +)
            if sum == 3 {
                winner = x
            }
            else if sum == -3 {
                winner = o
            }
        }
        
        return winner
    }
}