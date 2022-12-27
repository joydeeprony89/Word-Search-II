// See https://aka.ms/new-console-template for more information



/// https://www.youtube.com/watch?v=asbcE9mZz_U&list=PLot-Xpze53ldg4pN6PfzoJY7KsKcxF1jg&index=31
/// 


Console.WriteLine("Hello, World!");
Solution s = new Solution();
var board = new char[2][] { new char[] {'a', 'c' }, new char[] { 'p', 'e' } };
var words = new string[] { "app", "ape", "ace", "dog", "pac"};
var answer = s.FindWords(board, words); 

foreach(var a in answer)
  Console.WriteLine(a);
public class Trie
{
  public Trie[] Children;
  public bool IsWord;
  public Trie()
  {
    Children = new Trie[26];
  }

  public void Add(string word)
  {
    var current = this;
    foreach (char c in word)
    {
      if (current.Children[c - 'a'] == null) current.Children[c - 'a'] = new Trie();
      current = current.Children[c - 'a'];
    }

    current.IsWord = true;
  }
}



public class Solution
{
  public IList<string> FindWords(char[][] board, string[] words)
  {
    Trie root = new Trie();
    foreach (string word in words)
    {
      root.Add(word);
    }

    int row = board.Length;
    int column = board[0].Length;
    var result = new HashSet<string>();
    var visited = new HashSet<(int, int)>();
    for (int i = 0; i < row; i++)
    {
      for (int j = 0; j < column; j++)
      {
        Dfs(i, j, root, "");
      }
    }

    void Dfs(int r, int c, Trie node, string word)
    {
      if (r < 0 || c < 0 || r >= row || c >= column || visited.Contains((r, c)) || node.Children[board[r][c] - 'a'] == null) return;

      word += board[r][c];
      node = node.Children[board[r][c] - 'a'];
      if (node.IsWord) result.Add(word);
      visited.Add((r, c));
      Dfs(r + 1, c, node, word);
      Dfs(r - 1, c, node, word);
      Dfs(r, c + 1, node, word);
      Dfs(r, c - 1, node, word);
      visited.Remove((r, c));
    }

    return result.ToList();
  }
}