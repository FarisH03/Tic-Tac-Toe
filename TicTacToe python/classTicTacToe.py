class TicTacToe:
    def __init__(self):
        self.board = [str(i + 1) for i in range(9)]
        self.current_player = 'X'
        self.number_of_moves = 9
        self.play_game()

    def display_board(self):
        for i in range(len(self.board)):
            if i % 3 == 0 and i != 0:
                print()
            if i % 3 != 0 and i != 0:
                print("|", end="")
            print(self.board[i], end="")
        print()

    def play_game(self):
        self.display_board()
        while self.number_of_moves != 0 and not self.check_winner():
            self.handle_turn()
            if self.number_of_moves == 0 and not self.check_winner():
                print("\nIt's a draw")
                break

    def handle_turn(self):
        print(f"\n{self.current_player}'s turn")
        while True:
            try:
                user_input = int(input("\nSelect a field from 1-9: "))
                if 1 <= user_input <= 9 and self.board[user_input - 1].isdigit():
                    self.board[user_input - 1] = self.current_player
                    self.current_player = 'O' if self.current_player == 'X' else 'X'
                    self.number_of_moves -= 1
                    self.display_board()
                    break
                else:
                    print("Invalid input! Please enter a valid field.")
                    self.display_board()
            except ValueError:
                print("Invalid input! Please enter an integer.")
                self.display_board()

    def check_winner(self):
        winner = None
        b = self.board
        if ((b[0] == b[1] == b[2] != '-') or
            (b[3] == b[4] == b[5] != '-') or
            (b[6] == b[7] == b[8] != '-') or
            (b[0] == b[3] == b[6] != '-') or
            (b[1] == b[4] == b[7] != '-') or
            (b[2] == b[5] == b[8] != '-') or
            (b[0] == b[4] == b[8] != '-') or
            (b[2] == b[4] == b[6] != '-')):
            winner = 'O' if self.current_player == 'X' else 'X'

        if winner:
            print(f"\n{winner} won")
            return True
        return False
