# AzureAI.SpeechConcat

The purpose of this C# .NET 8 console application is to convert long text files into speech using the Azure AI Speech service. The solution has been implemented specifically to create audiobook-like audio files based on fiction writing.

The application will first add limited SSML markup to the provided plain-text file content. The following SSML markup will be introduced:

- Newline (`\r\n`) will create a new paragraph with a strong break in between paragraphs.
- Three consecutive asterisks (`***`) will create an extra long break (used when the scene changes in the fictional story).

Then, the SSML content will be split into chunks of a maximum of 5000 characters, ensuring the SSML elements stay intact. Why the limit of 5000 characters? It allows the text to be converted using either the Batch synthesis API or the `SpeechSynthesizer`, of which the latter is limited to 10 minutes of audio. You can select which method you wish to use via the `appsettings.json` file described later.

- When using the `SpeechSynthesizer`, the content batches are iterated consecutively, and the synthesized speech is simultaneously played through the computer's speakers. The synthesized audio is written into a separate `.wav` file for each of the completed batches. When all of the batches have been converted into speech, the partial audio files will be merged into a single file, and the partial files will be deleted.
- When using the Batch synthesis API, the content batches will be processed in parallel. The application will wait for all the created synthesis jobs to complete before downloading the synthesis results and cleaning up the jobs. The results are downloaded as `.zip` files, and their contents are then extracted. Finally, the extracted `.wav` files are merged into a single audio file, and both the downloaded zip files and the extracted folders containing the partial audio files are deleted.

## Running the application

1. Create a **Speech** service resource with the **Standard S0** pricing tier on your Azure subscription.
1. Clone this repository onto your computer and open the solution on Visual Studio.
1. Create an `appsettings.json` file in the project root. The file is not automatically present because it has been added to `.gitignore` to prevent the contents from being accidentally checked into version control.
1. Add the following keys to the `appsettings.json` file and populate them with your values.

   ```json
   {
       "SpeechKey": "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVQWXYZ", // Get this from the Speech service Overview blade
       "SpeechRegion": "westeurope", // Get this from the Speech service Overview blade
       "TextFilePath": "C:\\directory\\file.txt", // File containing the plain-text you wish to synthesize
       "VoiceName": "en-US-Ava:DragonHDLatestNeural", // https://speech.microsoft.com/portal/voicegallery
       "Temperature": "1.0", // 0.0 - 1.0, 0.0 = no randomness, 1.0 = maximum randomness
       "AudioFileName": "Output file name without extension", // Created in the same directory with the text file
       "UseBatchSynth": true // true = use the Batch synthesis API (async), false = use SpeechSynthesizer (real-time)
   }
   ```
1. Run the console application.
