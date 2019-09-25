using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource[] sounds;

    private void Start()
    {
        sounds = GetComponents<AudioSource>();

        ViveAction.getFairyWand += playFairyWandSound;
        ViveAction.getButtonDown += playButtonDown;
        ViveTexturePainter.spraying += playSprayingSound;

        BubbleBreak.bubbleBreak += playBubbleBreakSound;

        SprayNumber.spraying += playSprayingSound;
        SprayNumber.trainMoving += playTrainMovingSound;
        SprayNumber.doorOpen += playDoorOpenSound;

        GlassBreak.glassBreak += playGlassBreak;

        PuzzleDisplay.fountainWater += playFountainWaterSound;

        PinDown.isPinDown += playPinDownSound;

    }


    private void playFairyWandSound(object sender, EventArgs args)
    {
        sounds[0].Play();
    }

    private void playBubbleBreakSound(object sender, EventArgs args)
    {
        sounds[1].Play();
    }

    private void playSprayingSound(object sender, EventArgs args)
    {
        sounds[2].Play();
    }

    private void playGlassBreak(object sender, EventArgs args)
    {
        sounds[3].Play();
    }

    private void playButtonDown(object sender, EventArgs args)
    {
        sounds[4].Play();
    }

    private void playTrainMovingSound(object sender, EventArgs args)
    {
        sounds[5].Play();
    }

    private void playDoorOpenSound(object sender, EventArgs args)
    {
        sounds[6].Play();
    }

    private void playFountainWaterSound(object sender, EventArgs args)
    {
        sounds[7].Play();
    }

    private void playPinDownSound(object sender, EventArgs args)
    {
        sounds[8].Play();
    }
}
