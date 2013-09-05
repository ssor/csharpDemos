using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace RfidReader
{
    public class AudioAlert
    {
        public static void PlayAlert()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "./waves/alert.wav";
            player.Play();
        }
        public static void Msg()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "./waves/msg.wav";
            player.Play();
        }
    }
}
