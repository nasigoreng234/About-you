using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static readonly object lockObj = new object();

    static void AnimateText(string text, double delayInSeconds)
    {
        lock (lockObj)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep((int)(delayInSeconds * 1000)); // delay per karakter
            }
            Console.WriteLine();
        }
    }

    static void SingLyric(object obj)
    {
        var (lyric, startDelay, speed) = ((string, double, double))obj;

        Thread.Sleep((int)(startDelay * 1000)); // Delay sebelum teks muncul
        AnimateText(lyric, speed);
    }

    static void Main()
    {
        var lyrics = new List<(string lyric, double speed)>
        {
            ("Do you think I have forgotten?", 0.1),
            ("Do you think I have forgotten?", 0.1),
            ("Do you think I have forgotten", 0.1),
            ("about you?", 0.2),
            ("There was something bout you that now I cant remember", 0.08),
            ("Its the same damn thing that made my heart surrender", 0.1),
            ("And I miss you on a train, I miss you in the morning", 0.1),
            ("I never know what to think about", 0.1),
            ("I think about youuuuuuuuuuuuuuuuuuuuuuuuuuu", 0.1)
        };

        var delays = new List<double> { 0.3, 5.0, 10.0, 15.0, 20.3, 25.0, 27.0, 30.2, 33.3 };

        List<Thread> threads = new List<Thread>();

        for (int i = 0; i < lyrics.Count; i++)
        {
            var lyric = lyrics[i].lyric;
            var speed = lyrics[i].speed;
            var delay = delays[i];

            Thread t = new Thread(SingLyric);
            t.Start((lyric, delay, speed));
            threads.Add(t);
        }

        foreach (var t in threads)
        {
            t.Join(); // Tunggu semua thread selesai
        }
    }
}
