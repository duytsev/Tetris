using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace TetrisGame.Main
{
    class SongPlayer
    {
        private Song menuSong;
        private Song soundtrack;
        private SoundEffect fallEffect;
        private SoundEffect fullRowEffect;
        private SoundEffect gameOverEffect;
        private SoundEffect nextLevelEffect;
        private SoundEffect rotateEffect;
        private SoundEffect selectEffect;
        private ContentManager Content;

        public SongPlayer(ContentManager Content)
        {
            this.Content = Content;
        }

        public void loadContent()
        {
            menuSong = Content.Load<Song>("music/menu");
            soundtrack = Content.Load<Song>("music/soundtrack");
            fallEffect = Content.Load<SoundEffect>("effects/fall");
            fullRowEffect = Content.Load<SoundEffect>("effects/fullrow");
            gameOverEffect = Content.Load<SoundEffect>("effects/gameover");
            nextLevelEffect = Content.Load<SoundEffect>("effects/nextlevel");
            rotateEffect = Content.Load<SoundEffect>("effects/rotate");
            selectEffect = Content.Load<SoundEffect>("effects/select");
        }

        public void mute()
        {
            MediaPlayer.IsMuted = (MediaPlayer.IsMuted) ? false : true;
        }

        public void stop()
        {
            MediaPlayer.Stop();
        }

        public void playMenuSong()
        {
            MediaPlayer.Volume = 1f;
            MediaPlayer.Play(menuSong);
            MediaPlayer.IsRepeating = true; 
        }

        public void playSoundtrack()
        {
            MediaPlayer.Volume = 1f;
            MediaPlayer.Play(soundtrack);
            MediaPlayer.IsRepeating = true; 
        }
        

        public void changeSong(Song song)
        {
            MediaPlayer.Volume = 1f;
            MediaPlayer.Play(song);
        }

        public void rotate()
        {
            if (!MediaPlayer.IsMuted)
            {
                rotateEffect.Play();
            }
        }

        public void land()
        {
            if (!MediaPlayer.IsMuted)
            {
                fallEffect.Play();
            }
        }

        public void fullRow()
        {
            if (!MediaPlayer.IsMuted)
            {
                fullRowEffect.Play();
            }
        }

        public void levelUp()
        {
            if (!MediaPlayer.IsMuted)
            {
                nextLevelEffect.Play();
            }
        }

        public void gameOver()
        {
            if (!MediaPlayer.IsMuted)
            {
                gameOverEffect.Play();
            }
        }
    }
}
