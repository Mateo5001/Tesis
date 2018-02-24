using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Services.Contract
{
  public interface IAudioPlayer
  {
    void Play(string pathToAudioFile);
    
    void MediaPlayer_Completion(object sender, EventArgs e);

    void Pause();

    void Play();

    void Stop();
  }
}
