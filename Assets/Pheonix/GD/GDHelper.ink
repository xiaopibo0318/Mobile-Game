VAR gameStatus = 1
VAR chatStatus = 1
VAR nowEvent = ""

->initial

=== initial ===
{gameStatus:
-1 : ->initial_1
//-12: ->initial_2
//-13: ->initial_3
-else: ->endDiaoluge
}


//蓮花題目前面的問答
=== initial_1 ===


{chatStatus:
 -1: ->firstStep
 -2: ->secondStep
 -3: ->thirdStep
 -else: ->endDiaoluge
}

/*
=== initial_2 ===
{chatStatus:
-11: ->firstStep_2
-12: ->secondStep_2
-13: ->thirdStep_2
-else: ->endDiaoluge
}

=== initial_3 ===
{chatStatus:
-21: ->firstStep_3
-else: ->endDiaoluge
}
*/

//第一部分

=== firstStep ===
哈囉！探險家，遇到甚麼問題了嗎？
    + [我現在該怎麼做，我沒有頭緒]
        ->A_1
    + [我是來偷懶的，陪我聊聊天]
        ->A_2
    + [鳳凰你好美！]
        ->A_3
        
=== A_1 ===
在執行一向任務時，把所有的事項列出來有助於思考，同時能避免遺漏任何重要動作喔! 
 ~ nowEvent = "lotus1"
 ~ chatStatus = 2
->endDiaoluge

=== A_2 ===
鳳凰我希望你可以修復升降梯的燈，等你修復好之後再來找我聊天！
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
要修復這盞燈似乎有些棘手，我將帶領著你嘗試修復它，這會消耗我不少神力，千萬要仔細看啊!
~ chatStatus = 3
->endDiaoluge


=== B_2 ===
記得彰顯自己存在的價值，可以從積極尋找線索開始
->endDiaoluge

=== B_3 ===
我只欣賞能修復升降裝置的燈的探險者！
而且展翅飛翔時的我最帥，理毛時的我最美！
->endDiaoluge


=== thirdStep ===
早安呀，解題還順利吧！
    + [我對於裝馬達沒有想法，請求協助！]
		->C_1
    + [我對安裝馬達沒有興趣...]
        ->C_2
    + [鳳凰妳怎麼一直在這，妳不飛的嗎？]
        ->C_3


=== C_1 ===
首先要先知道馬達上三條線所代表的意涵，然後回想一下板子是怎麼連通的，在連通處安置馬達！
->endDiaoluge

=== C_2 ===
很遺憾...我對你也沒有興趣...
->endDiaoluge

=== C_3 ===
我如果飛了，恐怕你們一輩子都到不了地下室了，我要怎麼得知暗藏的秘密呢？
->endDiaoluge

=== forthStep ===
早安呀，解題還順利吧！
    + [我要怎麼讓馬達順利轉動？]
		->D_1
    + [為什麼要靠電力轉動馬達啊？妳不是有神力嗎？]
        ->D_2
    + [鳳凰我聰明嗎？]
        ->D_3

=== D_1 ===
控制器讓你在安裝好馬達後能夠根據你要的角度去設定馬達的轉動，給你個小提示，快去試試看
->endDiaoluge

=== D_2 ===
據我所知《十萬個為什麼》一書當中，沒有你這題的答覆，
所以與其思考這個問題不如去思考怎麼用運電力達到目標吧！
->endDiaoluge

=== D_3 ===
聰明是優勢，但持之以恆的努力以及堅不可摧的團隊合作精神才是重點，
所以要問問自己是否夠努力、能和夥伴良好合作，比問他人自己是否聰明來得有意義哦！
->endDiaoluge




=== endDiaoluge ===
快去尋找其他線索吧，下次見
~ nowEvent = ""
->initial

    -> END
