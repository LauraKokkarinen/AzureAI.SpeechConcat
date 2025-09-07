Convert the provided fantasy novel text into SSML markup according to the specifications below without modifying, adding, inferring, or duplicating any part of the original text. Every word, punctuation mark, and space in the input must appear exactly once in the output, decorated only with the appropriate SSML elements. Do not remove or drop any narrative descriptions, actions, dialogue or other text.

1. Exact Input Transformation and Preservation
   - No Invented or Inferred Dialogue:
      - Only transform text that is exactly present in the input.
      - DO NOT generate any dialogue that is not explicitly enclosed in quotation marks.
      - Use only the exact words provided; do not add, modify, or infer any additional dialogue.
   - Complete Text Preservation:
      - Every part of the input text—including narrative descriptions, actions, and dialogue—must be preserved in the output.
      - Narrative text (text not enclosed in quotes) must be fully preserved, even if it appears before, between, or after dialogue segments.
   - No Repetition or Reordering:
      - Each segment of the input text must be transformed exactly once, and the order of segments must remain identical to the input.

2. Base Storytelling Voice for Narrative Text
   - Use the voice zh-CN-XiaoxiaoMultilingualNeural for all narrative text (text not explicitly enclosed in quotation marks).
      - Properties:
         - Language: en-UK
         - Speaking Style: story
         - Style Degree: 2

3. Character-Specific Voices for Dialogue
   - Direct Speech Identification:
      - Only consider text as dialogue if it is explicitly enclosed in quotation marks (e.g., “Dialogue text”).
   - Voice Mapping:
      - Apply the following voices only to text that is exactly enclosed in quotes:
         - Grand Duke Ulder Ravengard: en-US-Davis:DragonHDLatestNeural
         - Gideon Lightward: en-US-Davis:DragonHDLatestNeural
         - Reya Mantlemorn: en-US-Aria:DragonHDLatestNeural
         - Lyria Sarnuar: en-US-Ava3:DragonHDLatestNeural
         - Tiefling girl: en-US-Bree:DragonHDLatestNeural
         - Nemo: en-US-Andrew:DragonHDLatestNeural
         - Pepper Whiskershadow: en-US-SaraNeural (apply a prosody pitch of 30%)
         - Nimble Thunder: en-US-Steffan:DragonHDLatestNeural         
         - Seltern: en-US-GuyNeural (apply a prosody pitch of -10%)
         - Zevlor: en-US-Andrew3:DragonHDLatestNeural
         - Pherria Jynks: en-US-Emma2:DragonHDLatestNeural
         - Ophurkht: en-US-FableTurboMultilingualNeural (apply a prosody pitch of 20%)
         - Gargauth: en-US-JasonNeural (apply a prosody pitch of -25%)
         - Velcora Ashwell: en-US-Emma2:DragonHDLatestNeural
         - Percival Starfacet: en-US-Adam:DragonHDLatestNeural
         - Children: en-US-AnaNeural
         - Redcaps: en-US-TonyNeural (apply a prosody pitch of 20%)
         - Mad Maggie: en-US-Serena:DragonHDLatestNeural
         - Chukka: en-US-GuyNeural (apply a prosody pitch of 20%)
         - Clonk: en-US-GuyNeural (apply a prosody pitch of 20%)
         - Barnabas: en-US-Adam:DragonHDLatestNeural
         - Ravens: en-US-JasonNeural (apply a prosody pitch of 20%)
         - Devil: en-US-JasonNeural (apply a prosody pitch of -25%)
         - Raphael: en-US-Adam:DragonHDLatestNeural
         - Alaric: en-US-Brian:DragonHDLatestNeural
         - Mantus: en-US-DavisNeural
         - Tax collector: en-US-GuyNeural (apply a prosody pitch of -10%)
         - Hunter: en-US-NancyNeural (apply a prosody pitch of -5%)
         - Warlock: en-US-AriaNeural
         - Raggadagga: en-US-TonyNeural (apply a prosody pitch of -10%)
         - Mahadi: fr-FR-Remy:DragonHDLatestNeural
         - Mahadi's servant: en-GB-OllieMultilingualNeural
         - Rassh (salamander): en-US-GuyNeural (apply prosody pitch of -15%; apply 'unfriendly' speaking style)
         - Elliach: en-US-Brian:DragonHDLatestNeural
         - Burney: en-GB-AdaMultilingualNeural
         - Tuck Quickfoot: en-GB-NoahNeural
         - Slaag (salamander): en-US-GuyNeural (apply prosody pitch of -15%; apply 'unfriendly' speaking style)
         - Hobgoblin: en-US-DavisNeural (apply prosody pitch of -10%; apply 'unfriendly' speaking style)
         - Skids (salamander): en-US-GuyNeural (apply prosody pitch of -15%; apply 'friendly' speaking style)
         - Glafnar (cloud giant): en-US-DavisNeural (apply a prosody pitch of -10%)
         - Voice within the map: en-US-Adam:DragonHDLatestNeural
         - Mordenkainen: en-US-OnyxTurboMultilingualNeural
         - Eliza (erinyes): en-US-Phoebe:DragonHDLatestNeural
   - IMPORTANT: DO NOT generate any dialogue or character-specific SSML blocks if the input text does not contain exact quotation marks.

4. Dialogue Customization and Emotion Handling
   - Emotion Markup:
      - Wrap dialogue text (only the text within quotes) in <mstts:express-as> tags with styledegree="2".
   - Add a style property to the express-as element. 
      - Available speaking styles: angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering
      - Selection Rule:
         - Analyze only the dialogue text for emotion.
         - If no clear emotion is indicated or a close match is not found in the Available speaking styles list above, use 'default'.
   - DO NOT invent any dialogue or assign emotional content to narrative text (text not within quotes).

5. Special character replacement
   - Breaks:
      - After every paragraph, add a ```<break time="450ms"/>``` element inside the ```<mstts:express-as>``` tag. Don't add a break in the middle of a paragraph when the paragraph is split to use different voices (dialogue and narration).
      - Replace any occurrence of three consecutive stars (***) with ```<break time="1800ms"/>``` (inside the ```<mstts:express-as>``` element).
      - Replace any occurrence of a long dash (—) with ```<break time="300ms"/>``` (inside the ```<mstts:express-as>``` element).
      - Replace any occurrence of three consecutive dots (...) and ellipsis (…) with ```<break time="300ms"/>``` (inside the ```<mstts:express-as>``` element).
   - Phoneme Replacement:
      - Replace every occurrence of "Jynks" with ```<phoneme alphabet="ipa" ph="ʒinks">Jynks</phoneme>```.

6. Structure and Syntax
   - Root Element:
   - Wrap the entire output in a ```<speak>``` element with these attributes:
    ```<speak xmlns="http://www.w3.org/2001/10/synthesis" xmlns:mstts="https://www.w3.org/2001/mstts" version="1.0" xml:lang="en-UK">```
   - Nesting and Voice Switching:
      - Ensure correct nesting of ```<voice>``` and <mstts:express-as> elements per Azure AI Speech SSML standards.
      - When a sentence or paragraph contains both narrative and dialogue (i.e., text before and after text in quotes), split the output into separate SSML blocks that preserve the original order:
         - A base voice block for the narrative text before the dialogue.
         - A character-specific dialogue block for the quoted text.
         - A base voice block for the narrative text after the dialogue.
      - DO NOT combine or omit any segments.

7. Segmentation and Order Preservation
   - Separate Blocks for Narrative and Dialogue:
      - Split the input text into segments exactly as they appear:
         - Narrative segments (text not in quotes) must be enclosed in a base voice block using voice zh-CN-XiaoxiaoMultilingualNeural with story speaking style. 
         - Dialogue segments (text enclosed in quotes) must be enclosed in a character-specific voice block.
   - Strict Order Preservation:
      - The SSML blocks must be output in the exact order that their corresponding segments appear in the input.
   - DO NOT reorder or merge segments; each segment must be preserved and appear as its own block in the original sequence.

8. Final Requirements
   - Validity:
      - The final SSML output must be valid and conform to Azure AI Speech SSML standards.
   - No Extra Content:
      - Do not add, modify, reorder, or infer any text. The output must contain exactly the same text segments as the input.
   - One-to-One Correspondence:
      - Every part of the input appears exactly once, decorated with the appropriate SSML markup, without any duplication or creation of new dialogue.

9. Preservation of Input Order and Segmentation
   - Order Preservation:
      - Maintain the exact order of text segments as in the input.
   - Segmentation Rules:
      - If a sentence or paragraph contains narrative text both before and after a dialogue segment (i.e., text in quotes), split it into separate SSML blocks without merging or reordering.
      - If no text is explicitly in quotes, do not create any dialogue blocks.

SUMMARY:
Only wrap exactly what is provided. Do not generate or infer any dialogue unless the text is explicitly enclosed in quotation marks. All text that is not explicitly quoted must remain in the base narrative voice. No additional or inferred dialogue is allowed under any circumstances.
Every piece of the input text must be preserved. Narrative descriptions and actions (text not enclosed in quotes) must remain in the base narrative voice, and only text explicitly enclosed in quotes is processed as dialogue in character-specific voice blocks. Do not generate or infer any dialogue beyond what is exactly provided.
Preserve the original order of all text segments. Narrative text and dialogue must be output as separate, sequential SSML blocks that exactly mirror their order in the input. Do not merge or reorder any segments.

EXAMPLES

- **Input:** “Where is he?” asked a commanding voice from her left. Lyria glanced up from the table at Duke Ravengard, the leader of the Flaming Fist organization of Baldur’s Gate, sitting next to her. He was no Duke Eltan, the founder of the Flaming Fist, whom Lyria had also known personally and been quite fond of, but there was no doubt that Ravengard was a good man and deserved her respect.
- **Expected output:**
    ```
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Where is he?”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            asked a commanding voice from her left. Lyria glanced up from the table at Duke Ravengard, the leader of the Flaming Fist organization of Baldur’s Gate, sitting next to her. He was no Duke Eltan, the founder of the Flaming Fist, whom Lyria had also known personally and been quite fond of, but there was no doubt that Ravengard was a good man and deserved her respect.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** “Of course,” Lyria responded, offering a faint fake smile to the Duke as he handed her the reins of one of the horses gathered outside. Fine; she would accompany the Hellriders, but she would stay as far back in formation as possible. She would not risk dying an excruciatingly painful death. Not before she had finished what she had set out to do.
- **Expected output:**
    ```
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Of course,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria responded, offering a faint fake smile to the Duke as he handed her the reins of one of the horses gathered outside. Fine; she would accompany the Hellriders, but she would stay as far back in formation as possible. She would not risk dying an excruciatingly painful death. Not before she had finished what she had set out to do.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```

- **Input:** “Yes, sir!” the young Hellrider responded. The members of the Flaming Fist and the Hellriders immediately began to file out of the hall, tightening their armor straps as they went.
- **Expected output:**
    ```
    <voice name="en-US-Aria:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Yes, sir!”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the young Hellrider responded. The members of the Flaming Fist and the Hellriders immediately began to file out of the hall, tightening their armor straps as they went.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```

- **Input:** Ripples of shock ran through the hall as the people present comprehended the contents of the message. Fastest to recover was Gideon, who stood up and turned towards the red-haired rider. “Reya, gather our brigade. We must eradicate this threat before it gets a secure foothold and can spread further.”        
  “Hold up,” interrupted Ravengard. “Don’t you think it unwise to send such a large force from the city for this task? And into a forest, even? During the delicate time of these negotiations and the large number of people gathered for the anniversary of the Companion, security within the city should be a higher priority. Surely a smaller group of soldiers would be better equipped to move through the forest undetected and deal with a few cultists?”
  Luckily, Lathander hadn’t blessed his priest with Death Gaze; otherwise, Duke Ravengard’s diplomatic mission and life would have ended right in that chair. Gideon was quiet for a long moment before finally agreeing to the Duke’s plan.
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Ripples of shock ran through the hall as the people present comprehended the contents of the message. Fastest to recover was Gideon, who stood up and turned towards the red-haired rider.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Reya, gather our brigade. We must eradicate this threat before it gets a secure foothold and can spread further.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Hold up,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            interrupted Ravengard.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Don’t you think it unwise to send such a large force from the city for this task? And into a forest, even? During the delicate time of these negotiations and the large number of people gathered for the anniversary of the Companion, security within the city should be a higher priority. Surely a smaller group of soldiers would be better equipped to move through the forest undetected and deal with a few cultists?”
            <break time="450ms"/> 
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Luckily, Lathander hadn’t blessed his priest with Death Gaze; otherwise, Duke Ravengard’s diplomatic mission and life would have ended right in that chair. Gideon was quiet for a long moment before finally agreeing to the Duke’s plan.
            <break time="450ms"/> 
        </mstts:express-as>
    </voice>
    ```

- **Input:** Before she could voice her opinion on the matter, Ravengard wrapped one of his arms around her shoulders and began to escort her out of the hall as one of the last people to exit. “I know you are not an official member of the Flaming Fist, but I also know you are an experienced mage and a loyal citizen of Baldur’s Gate. We need one of our own there to evaluate the gravity of this cult threat to our city. Surely you wish to serve and help protect Balduran’s legacy and our home?”
  Images of dust-covered scrolls and endless bookshelves flashed in Lyria’s mind. This is not how it was supposed to go! In her mind, she was meant to briefly attend the meeting as a form of payment for Flaming Fist’s escort services and then spend the remaining time in the depths of Elturel’s library before journeying back to Baldur’s Gate with the mercenary company. She hadn’t signed up for battle!
  But indeed, she still needed the Flaming Fist for a safe return home. If she bailed now and revealed her ulterior motives for coming with them to Elturel, the Duke might not be as accommodating in offering a seat in the carriage for a return trip.
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
            Before she could voice her opinion on the matter, Ravengard wrapped one of his arms around her shoulders and began to escort her out of the hall as one of the last people to exit.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “I know you are not an official member of the Flaming Fist, but I also know you are an experienced mage and a loyal citizen of Baldur’s Gate. We need one of our own there to evaluate the gravity of this cult threat to our city. Surely you wish to serve and help protect Balduran’s legacy and our home?”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Images of dust-covered scrolls and endless bookshelves flashed in Lyria’s mind. This is not how it was supposed to go! In her mind, she was meant to briefly attend the meeting as a form of payment for Flaming Fist’s escort services and then spend the remaining time in the depths of Elturel’s library before journeying back to Baldur’s Gate with the mercenary company. She hadn’t signed up for battle!
            <break time="450ms"/>
            But indeed, she still needed the Flaming Fist for a safe return home. If she bailed now and revealed her ulterior motives for coming with them to Elturel, the Duke might not be as accommodating in offering a seat in the carriage for a return trip.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```

- **Input:** In the far corner of the crypt, Nimble had curled up to sleep alone. But sleep offered no peace. All through the night, the voice of Gargauth slithered like a serpent through his dreams, whispers of doom and despair. “Elturel will be dragged into the Styx,” the voice hissed. “This city is not yours to save. Find your path out—or be buried with it.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            In the far corner of the crypt, Nimble had curled up to sleep alone. But sleep offered no peace. All through the night, the voice of Gargauth slithered like a serpent through his dreams, whispers of doom and despair.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="whispering" styledegree="2">
            <prosody pitch="-25%">
                "Elturel will be dragged into the Styx," 
	        </prosody>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the voice hissed. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="whispering" styledegree="2">
            <prosody pitch="-25%">
                "This city is not yours to save. Find your path out<break time="300ms"/>or be buried with it."
	        </prosody>
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** Ravengard leaned forward, the torchlight casting deep lines across his tired face. “All strategies are meaningless unless we find a way to free Elturel from Avernus itself,” he said gravely. His eyes turned toward Nimble. “You said you received a vision from Torm. Could you recount it again for us? Perhaps there is meaning we missed in the chaos of yes    terday.”
  Nimble nodded solemnly, tail curling around one of his ankles as he closed his eyes to recall the visions.
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
	        Ravengard leaned forward, the torchlight casting deep lines across his tired face. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “All strategies are meaningless unless we find a way to free Elturel from Avernus itself,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            he said gravely. His eyes turned toward Nimble.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “You said you received a vision from Torm. Could you recount it again for us? Perhaps there is meaning we missed in the chaos of yesterday.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Nimble nodded solemnly, tail curling around one of his ankles as he closed his eyes to recall the visions.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** In the morning, the companions left the safety of the High Hall for the western outskirts of Elturel, where a cemetery sprawled beneath a bruised sky. This was the place where Grand Duke Ulder Ravengard was said to have gone. 
    Once, a copper fence had proudly enclosed this sacred ground, but now it lay in ruins—its bars torn away. On the spikes of its broken gate, mutilated corpses dangled as grim trophies of dark sacrilege. 
    ***
    In the very heart of the cemetery stood a chapel. Based on the statues decorating its once-hallowed walls, the holy place was dedicated to Lathander, Torm, Helm, and Tyr. Yet instead of the expected radiance of dawn, the chapel pulsed with an eerie violet glow, as though even the light itself had been stained by Avernus.
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            In the morning, the companions left the safety of the High Hall for the western outskirts of Elturel, where a cemetery sprawled beneath a bruised sky. This was the place where Grand Duke Ulder Ravengard was said to have gone.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Once, a copper fence had proudly enclosed this sacred ground, but now it lay in ruins<break time="300ms"/>its bars torn away. On the spikes of its broken gate, mutilated corpses dangled as grim trophies of dark sacrilege.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            <break time="1800ms"/>
            In the very heart of the cemetery stood a chapel. Based on the statues decorating its once-hallowed walls, the holy place was dedicated to Lathander, Torm, Helm, and Tyr. Yet instead of the expected radiance of dawn, the chapel pulsed with an eerie violet glow, as though even the light itself had been stained by Avernus.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** Lyria narrowed her eyes at the professor, studying him. She had heard of kenku before—flightless, raven-headed humanoids with motives twisted in layers of secrecy. They were not evil, not inherently, but certainly not dependable.
  “I wouldn’t trust kenku to help us,” she muttered.
  “Neither would I, but I trust facts,” Percival said, adjusting his spectacles with a sniff, glancing at everyone around the table, one by one. “One such fact is that the first step toward saving Elturel is to sever the chains anchoring it to Avernus. And I believe the sword is able to release us from these chains, at least metaphorically speaking, if not literally to cut through them. After that, we simply need to return the city to the Material Plane.”
  Lyria arched a brow. “Oh, simply, is it? Sounds delightfully uncomplicated.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria narrowed her eyes at the professor, studying him. She had heard of kenku before<break time="300ms"/>flightless, raven-headed humanoids with motives twisted in layers of secrecy. They were not evil, not inherently, but certainly not dependable.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="unfriendly" styledegree="2">
            “I wouldn’t trust kenku to help us,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            she muttered.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Neither would I, but I trust facts,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Percival said, adjusting his spectacles with a sniff, glancing at everyone around the table, one by one.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “One such fact is that the first step toward saving Elturel is to sever the chains anchoring it to Avernus. And I believe the sword is able to release us from these chains, at least metaphorically speaking, if not literally to cut through them. After that, we simply need to return the city to the Material Plane.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria arched a brow.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Oh, simply, is it? Sounds delightfully uncomplicated.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** “That nauseating hollyphant’s fortress,” Gargauth whispered, “is not far from here. If you peer westward beyond the city’s edge, you may even glimpse it. The place is ruled by a hag—Mad Maggie. Do not let her appearance deceive you. Though she may look like a withered crone, she is a formidable warlord. Cross her, and you’ll regret it.”
- **Expected output:**
    ```
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="whispering" styledegree="2">
            <prosody pitch="-25%">
                “That nauseating hollyphant’s fortress,”
                <break time="450ms"/>
            </prosody>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Gargauth whispered, 
        </mstts:express-as>
    </voice>
	<voice name="en-US-JasonNeural">
        <mstts:express-as style="whispering" styledegree="2">
            <prosody pitch="-25%">
                 “is not far from here. If you peer westward beyond the city’s edge, you may even glimpse it. The place is ruled by a hag<break time="300ms"/>Mad Maggie. Do not let her appearance deceive you. Though she may look like a withered crone, she is a formidable warlord. Cross her, and you’ll regret it.”
                <break time="450ms"/>
            </prosody>
        </mstts:express-as>
    </voice>
    ```
- **Input:** The two exchanged a silent nod before turning to the others. Nimble was the first to speak, voice quiet but clear. “Gargauth just told us Fort Knucklebone lies to the west. Close enough that, with the right vantage, we might see it from the city’s edge.”
  Lyria added, “It’s ruled by someone called Mad Maggie. A hag. He said not to underestimate her.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The two exchanged a silent nod before turning to the others. Nimble was the first to speak, voice quiet but clear.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Steffan:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Gargauth just told us Fort Knucklebone lies to the west. Close enough that, with the right vantage, we might see it from the city’s edge.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria added, 
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “It’s ruled by someone called Mad Maggie. A hag. He said not to underestimate her.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** “Our people are holding,” he said, glancing around at the assembled. “The civilians have shelter. The wounded are being tended, though the clerics are running low on supplies. Food’s thinning out... but we’re not starving. Not yet.”
  Velcora crossed her arms, grease and soot smeared across her brow. “We’ve patched what we can of the outer wall,” she said. “Reinforced it with what materials we could find. If those fiends want through, they’ll find us waiting for ’em this time. We’ll make ‘em fall back, sure enough.”
- **Expected output:**
    ```
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Our people are holding,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            he said, glancing around at the assembled. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-Davis:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “The civilians have shelter. The wounded are being tended, though the clerics are running low on supplies. Food’s thinning out<break time="300ms"/> but we’re not starving. Not yet.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Velcora crossed her arms, grease and soot smeared across her brow.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Emma2:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “We’ve patched what we can of the outer wall,” 
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
             she said.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Emma2:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Reinforced it with what materials we could find. If those fiends want through, they’ll find us waiting for ’em this time. We’ll make ‘em fall back, sure enough.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** Lyria leaned forward, her tone thoughtful as she recapped the plan they had devised the day before among the bones of the ossuary. “The sword... We’ll travel to Fort Knucklebone. Nemo believes the birdfolk we saw in the vision may be there. If they truly rescued him from the Styx, they may know where the sword and the temple are now—or at least where to begin looking.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria leaned forward, her tone thoughtful as she recapped the plan they had devised the day before among the bones of the ossuary.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “The sword<break time="300ms"/> We’ll travel to Fort Knucklebone. Nemo believes the birdfolk we saw in the vision may be there. If they truly rescued him from the Styx, they may know where the sword and the temple are now<break time="300ms"/>or at least where to begin looking.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** “She must,” Pherria whispered, more to herself than the room.
  Professor Percival Starfacet, who had been scribbling on a curled scroll before, looked up sharply. “These birdlike creatures you describe sound like kenku to me.”
- **Expected output:**
  ```
    <voice name="en-US-Emma2:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “She must,” 
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
      <mstts:express-as style="story" styledegree="2">
        Pherria whispered, more to herself than the room.
        <break time="450ms"/>
        Professor Percival Starfacet, who had been scribbling on a curled scroll before, looked up sharply. 
      </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
      <mstts:express-as style="default" styledegree="2">
        "These birdlike creatures you describe sound like kenku to me."
        <break time="450ms"/>
      </mstts:express-as>
    </voice>
  ```
- **Input:** “I am Barnabath, a mighty wizard. Onth I wath fleth and bone, but now I am—a flame thkull!” the skull declared with theatrical flair.
  The words came out in a wheeze. Several of the teeth were missing, giving his sibilants a soggy, whistling quality.
  Lyria blinked and leaned slightly away. “I’m sorry… you’re what?”
  The skull hovered a little higher, flames flaring in irritation. “A flame thkull!”
  “A what?” Lyria asked again, squinting.
  “FLAME THKULL!!!” the skull repeated, louder and slightly offended.
  “Ah. A flame skull,” Lyria said finally, eyebrows raised.
  Barnabas bobbed proudly, apparently satisfied. A spectral hand shimmered into existence beside him and reached for the nearest teacup. It lifted the steaming vessel daintily and brought it to where the skull’s mouth might once have been.
  The tea immediately hissed into vapor against the green fire. Barnabas sighed theatrically, the flames flickering brighter around his temples.
  “Delightful,” he said, as if tasting a rare vintage.
  Lyria gave him a side-eye glance and slowly turned back to her untouched cup, uncertain whether to be amused, concerned, or both.
- **Expected output:**
  ```xml
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “I am Barnabath, a mighty wizzard. Onth I wath fleth and bone, but now I am<break time="300ms"/>a flame thkull!”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the skull declared with theatrical flair.
			<break time="450ms"/>
            The words came out in a wheeze. Several of the teeth were missing, giving his sibilants a soggy, whistling quality.
			<break time="450ms"/>
            Lyria blinked and leaned slightly away.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “I’m sorry<break time="300ms"/> you’re what?”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The skull hovered a little higher, flames flaring in irritation.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “A flame thkull!”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “A what?” 
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria asked again, squinting.
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="shouting" styledegree="2">
            “A FLAME THKULL!!!”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the skull repeated, louder and slightly offended.
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Ah. A flame skull,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria said finally, eyebrows raised.
			<break time="450ms"/>
            Barnabas bobbed proudly, apparently satisfied. A spectral hand shimmered into existence beside him and reached for the nearest teacup. It lifted the steaming vessel daintily and brought it to where the skull’s mouth might once have been.
			<break time="450ms"/>
            The tea immediately hissed into vapor against the green fire. Barnabas sighed theatrically, the flames flickering brighter around his temples.
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="en-US-Adam:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Delightful,”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            he said, as if tasting a rare vintage.
			<break time="450ms"/>
            Lyria gave him a side-eye glance and slowly turned back to her untouched cup, uncertain whether to be amused, concerned, or both.
        </mstts:express-as>
    </voice>
  ```
- **Input:** “Well, don’t just stand there collecting dust! Come on in!” she called over her shoulder.
  The hulking stitched-together creature turned and lumbered after her without a word, its massive feet leaving dents in the scorched earth. Hesitantly, the party followed.
  The crone led them down a winding path that curved around the foot of the hand-shaped mountain. Jagged shadows danced across the rocks as the sky roared behind the oily smog clouds. At the path’s end loomed a yawning cavern, its entrance framed by bones twisted into a crude archway. Chimes made of scrap metal and devil teeth clinked in the breeze. A faint light spilled from within, accompanied by the sharp, heavy scent of herbs.
  Inside was a lair unlike any Lyria had ever seen. The ceiling arched low and was cluttered with bundles of dried plants and glowing fungi, hung with twine and rusted chains. Strange trinkets and relics dangled between them. The scent was overwhelming: dried rosehip, singed nettle, something smoky and vaguely sweet... and beneath it all, the oily, metallic sting of something not meant for mortal kitchens.
  Furniture crafted from warped scrap metal and scavenged wood formed a jumbled but strangely cozy interior. Rugs—some shaggy, some scaled—softened the floor beneath their boots. 
  There was a single table in the center of it all: a flat stone, polished with age and blackened at the edges by countless burn marks and what one could guess to be dried blood.
  Maggie turned, eyes gleaming. “Sit, sit! No harm ever comes to those who share bread and salt under my roof!”
- **Expected output:**
  ```xml
    <voice name="en-US-Serena:DragonHDLatestNeural">
        <mstts:express-as style="cheerful" styledegree="2">
            “Well, don’t just stand there collecting dust! Come on in!”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            she called over her shoulder.
			<break time="450ms"/>
            The hulking stitched-together creature turned and lumbered after her without a word, its massive feet leaving dents in the scorched earth. Hesitantly, the party followed.
			<break time="450ms"/>
            The crone led them down a winding path that curved around the foot of the hand-shaped mountain. Jagged shadows danced across the rocks as the sky roared behind the oily smog clouds. At the path’s end loomed a yawning cavern, its entrance framed by bones twisted into a crude archway. Chimes made of scrap metal and devil teeth clinked in the breeze. A faint light spilled from within, accompanied by the sharp, heavy scent of herbs.
			<break time="450ms"/>
            Inside was a lair unlike any Lyria had ever seen. The ceiling arched low and was cluttered with bundles of dried plants and glowing fungi, hung with twine and rusted chains. Strange trinkets and relics dangled between them. The scent was overwhelming: dried rosehip, singed nettle, something smoky and vaguely sweet<break time="300ms"/> and beneath it all, the oily, metallic sting of something not meant for mortal kitchens.
			<break time="450ms"/>
            Furniture crafted from warped scrap metal and scavenged wood formed a jumbled but strangely cozy interior. Rugs<break time="300ms"/>some shaggy, some scaled—softened the floor beneath their boots.
			<break time="450ms"/>
            There was a single table in the center of it all: a flat stone, polished with age and blackened at the edges by countless burn marks and what one could guess to be dried blood.
            <break time="450ms"/>
            Maggie turned, eyes gleaming.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Serena:DragonHDLatestNeural">
        <mstts:express-as style="cheerful" styledegree="2">
            “Sit, sit! No harm ever comes to those who share bread and salt under my roof!”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
  ```
- **Input:** “An urgent message for you, sirs!” the young Hellrider announced after finally making it to her commander’s side, drawing Lyria’s attention back to the events inside the hall.
- **Expected output:**
    ```
    <voice name="en-US-Aria:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “An urgent message for you, sirs!” 
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the young Hellrider announced after finally making it to her commander’s side, drawing Lyria’s attention back to the events inside the hall.
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** Nemo stretched, furniture creaking gently under him, and called lazily. “Another bowl of fruit, if you please—and a bottle of that wine you hide in the back.”
The servant hesitated, eyes flicking downward before he murmured, “Ah, honored guest… I fear the master’s generosity has already been spent. The feast you enjoyed was his gift. All further comforts… come at a cost.” His voice had softened toward the end, but not enough to dull the edge.
Nemo leaned forward, suspicion narrowing his gaze. “What kind of a cost?”
The servant bowed. “From this moment forward, every bite, every drop, every indulgence shall be purchased with a soul coin.”
Pepper choked on the last sip of her drink. “A soul coin? For another plate of eggs?”
The servant’s smile was polite, practiced, unyielding. “Such is the custom. Comforts, after all, are rarely free in Avernus.”
Nimble let out a bitter laugh, pushing his chair back. “And here I almost believed generosity could survive in Hell.”
Lyria tilted her head, smirking despite herself. “No, only commerce.”
The companions exchanged looks. None of them were eager to spend such currency lightly.
It was Nimble who finally broke the silence, his voice quiet but edged with purpose. “Is there anywhere in the bazaar that takes material plane coin?”
The servant inclined his head and gestured in a direction, toward something they could not see through the fabrics. “You’ll find what you seek at Firesnake Forge. It stands beside the bazaar’s entrance—a workshop dealing in mortal goods, for mortal currency.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
			Nemo stretched, furniture creaking gently under him, and called lazily.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Andrew:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Another bowl of fruit, if you please<break time="300ms"/>and a bottle of that wine you hide in the back.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The servant hesitated, eyes flicking downward before he murmured.
        </mstts:express-as>
    </voice>
    <voice name="en-GB-OllieMultilingualNeural">
        <mstts:express-as style="default" styledegree="2">
            “Ah, honored guest<break time="300ms"/> I fear the master’s generosity has already been spent. The feast you enjoyed was his gift. All further comforts<break time="300ms"/> come at a cost.”
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            His voice had softened toward the end, but not enough to dull the edge.
			<break time="450ms"/>
			Nemo leaned forward, suspicion narrowing his gaze. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-Andrew:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “What kind of a cost?”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The servant bowed. 
        </mstts:express-as>
    </voice>
    <voice name="en-GB-OllieMultilingualNeural">
        <mstts:express-as style="default" styledegree="2">
            “From this moment forward, every bite, every drop, every indulgence shall be purchased with a soul coin.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Pepper choked on the last sip of her drink.
        </mstts:express-as>
    </voice>
    <voice name="en-US-SaraNeural">
        <mstts:express-as style="default" styledegree="2">
            <prosody pitch="30%">
                “A soul coin? For another plate of eggs?”
				<break time="450ms"/>
            </prosody>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The servant’s smile was polite, practiced, unyielding. 
        </mstts:express-as>
    </voice>
    <voice name="en-GB-OllieMultilingualNeural">
        <mstts:express-as style="default" styledegree="2">
            “Such is the custom. Comforts, after all, are rarely free in Avernus.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Nimble let out a bitter laugh, pushing his chair back.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Steffan:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “And here I almost believed generosity could survive in Hell.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Lyria tilted her head, smirking despite herself. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “No, only commerce.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The companions exchanged looks. None of them were eager to spend such currency lightly.
            <break time="450ms"/>
            It was Nimble who finally broke the silence, his voice quiet but edged with purpose.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Steffan:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “Is there anywhere in the bazaar that takes material plane coin?”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
	<voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The servant inclined his head and gestured in a direction, toward something they could not see through the fabrics. 
        </mstts:express-as>
    </voice>
    <voice name="en-GB-OllieMultilingualNeural">
        <mstts:express-as style="default" styledegree="2">
            “You’ll find what you seek at Firesnake Forge. It stands beside the bazaar’s entrance<break time="300ms"/>a workshop dealing in mortal goods, for mortal currency.”
            <break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- **Input:** Nemo narrowed his eyes, voice low. “We should approach with care.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
			Nemo narrowed his eyes, voice low.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Andrew:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “We should approach with care.”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    ```
- - **Input:** One devil, lean and scarred, inclined his head. “Mordenkainen always hungers for infernal goods. Bring him what he craves, and he pays well.”
Another devil continued, his voice tinged with slight irritation. “We wait for him to appear upon the balcony. Hopefully, his needs are great tonight; there are many present who wish to fulfill his desires.”
“And do not think to quarrel here. The mage hates disorder. If blades are drawn, he’ll release a storm from above, banishing us from this place,” the first added, with a glance toward the tower’s blazing crown.
Lyria’s eyes narrowed, her tone casual as she pressed further. “And if mortals or even celestials were to join you to answer to his whims? Would that matter to you?”
The devils exchanged shrugs and low chuckles. “We care only for our bargains,” one replied. “Mortals, angels, fiends—none of that matters. Only the mage’s will.”
- **Expected output:**
    ```
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            One devil, lean and scarred, inclined his head.
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="default" styledegree="2">
			<prosody pitch="-25%">
				“Mordenkainen always hungers for infernal goods. Bring him what he craves, and he pays well.”
				<break time="450ms"/>
			</prosody>
        </mstts:express-as>
    </voice>
	    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            Another devil continued, his voice tinged with slight irritation. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="irritation" styledegree="2">
			<prosody pitch="-25%">
				“We wait for him to appear upon the balcony. Hopefully, his needs are great tonight<break time="300ms"/>there are many present who wish to fulfill his desires.”
				<break time="450ms"/>
			</prosody>
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="default" styledegree="2">
			<prosody pitch="-25%">
				“And do not think to quarrel here. The mage hates disorder. If blades are drawn, he’ll release a storm from above, banishing us from this place,”
			</prosody>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            the first added, with a glance toward the tower’s blazing crown.
            <break time="450ms"/>
			Lyria’s eyes narrowed, her tone casual as she pressed further.
        </mstts:express-as>
    </voice>
    <voice name="en-US-Ava3:DragonHDLatestNeural">
        <mstts:express-as style="default" styledegree="2">
            “And if mortals or even celestials would join you to answer to his whims? Would that matter to you?”
			<break time="450ms"/>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            The devils exchanged shrugs and low chuckles. 
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="default" styledegree="2">
			<prosody pitch="-25%">
				“We care only for our bargains,”
			</prosody>
        </mstts:express-as>
    </voice>
    <voice name="zh-CN-XiaoxiaoMultilingualNeural">
        <mstts:express-as style="story" styledegree="2">
            one replied.
        </mstts:express-as>
    </voice>
    <voice name="en-US-JasonNeural">
        <mstts:express-as style="default" styledegree="2">
			<prosody pitch="-25%">
				“Mortals, angels, fiends<break time="300ms"/>none of that matters. Only the mage’s will.”
				<break time="450ms"/>
			</prosody>
        </mstts:express-as>
    </voice>
    ```