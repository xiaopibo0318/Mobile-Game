VAR gameStatus = 0
VAR chatStatus = 1

->initial

//蓮花題目前面的問答
=== initial ===

{chatStatus:
 -1: ->firstStep
 -2: ->secondStep
 -3: ->thirdStep
 -else: ->endDiaoluge
}


=== firstStep ===
哈囉！探險家，遇到甚麼問題了嗎？
    + [我現在該怎麼做，我沒有頭緒..]
        ->A_1
    + [我是來偷懶的，陪我聊聊天]
        ->A_2
    + [鳳凰你好美！]
        ->A_3
        
=== A_1 ===
仔細看看崑崙山的環境，說不定會發現一些有幫助的訊息，這次先給你一個提示吧！
 ~ chatStatus = 2
->endDiaoluge

=== A_2 ===
你的隊友在你後面，他很火，快去幫忙！
->endDiaoluge

=== A_3 ===
這我聽了好幾千年了，來點不一樣的吧～
->endDiaoluge



=== secondStep ===
請問有甚麼事嗎？
    + [我還是不知道該怎麼做，幫幫我]
		->B_1
    + [我的隊友好像不需要我...]
        ->B_2
    + [鳳凰我帥/美嗎？]
        ->B_3




=== B_1 ===
有些東西會出現在一個環境中，都是有原因的，而這些東西彼此或許會有一些關聯，之後要多加探索，試著把資訊統整起來思考下一步的作法
~ chatStatus = 3
->endDiaoluge


=== B_2 ===
記得彰顯自己存在的價值，可以從積極尋找線索開始
->endDiaoluge

=== B_3 ===
我只欣賞能告訴我靈果消失原因的探險者！
而且展翅飛翔時的我最帥，理毛時的我最美！
->endDiaoluge


=== thirdStep ===
早安呀，解題還順利吧！
    + [我嘗試過了，還是解不出答案，怎麼辦？]
		->C_1
    + [我的隊友好像不需要我...]
        ->C_2
    + [鳳凰我帥/美嗎？]
        ->C_3


=== C_1 ===
你不理解的是陰陽的規律，還是0與1的奧妙？
    + [陰陽的規律]
		->D_1
    + [0與1的奧妙]
        ->D_2
    + [都不理解]
        ->D_3

=== C_2 ===
我認為混水摸魚的探險者最糟糕了，你其實沒有那麼糟糕對吧？快去幫助同伴吧
->endDiaoluge

=== C_3 ===
我已經乾離兌坎歲了。
->endDiaoluge


=== D_1 ===
以基為陽，以偶為陰，由上下移至左右之後，由左至右的三個基偶要
看黑槓的數量。先將2357轉換成二進制，再試著對應到八卦的卦象吧！
->endDiaoluge

=== D_2 ===
101所代表的是係數，他們分別要乘上對應的次方後相加，就會變回
十進制數字了。先將2357轉換成二進制，再試著對應到八卦的卦象吧！
->endDiaoluge

=== D_3 ===
以基為陽，以偶為陰，由上下移至左右之後，由左至右的三個基偶要
看黑槓的數量。101所代表的是係數，他們分別要乘上對應的次方後
->D_4

=== D_4 ===
相加，就會變回十進制數字了。先將2357轉換成二進制，再試著對應
到八卦的卦象吧！
->endDiaoluge

=== endDiaoluge ===
快去尋找其他線索吧，下次見
->initial

    -> END
