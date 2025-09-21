using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;
using VRCFaceTracking.Core.Params.Data;

namespace ALVRModule
{
    using static FacePico;

    public enum FacePico
    {
        EyeLookDownL = 0,
        NoseSneerL = 1,
        EyeLookInL = 2,
        BrowInnerUp = 3,
        BrowDownR = 4,
        MouthClose = 5,
        MouthLowerDownR = 6,
        JawShapeOpen = 7,
        MouthUpperUpR = 8,
        MouthShrugUpper = 9,
        MouthFunnel = 10,
        EyeLookInR = 11,
        EyeLookDownR = 12,
        NoseSneerR = 13,
        MouthRollUpper = 14,
        JawShapeRight = 15,
        BrowDownL = 16,
        MouthShrugLower = 17,
        MouthRollLower = 18,
        MouthSmileL = 19,
        MouthPressL = 20,
        MouthSmileR = 21,
        MouthPressR = 22,
        MouthDimpleR = 23,
        MouthLeft = 24,
        JawShapeForward = 25,
        EyeSquintL = 26,
        MouthFrownL = 27,
        EyeBlinkL = 28,
        CheekSquintL = 29,
        BrowOuterUpL = 30,
        EyeLookUpL = 31,
        JawShapeLeft = 32,
        MouthStretchL = 33,
        MouthPucker = 34,
        EyeLookUpR = 35,
        BrowOuterUpR = 36,
        CheekSquintR = 37,
        EyeBlinkR = 38,
        MouthUpperUpL = 39,
        MouthFrownR = 40,
        EyeSquintR = 41,
        MouthStretchR = 42,
        CheekPuff = 43,
        EyeLookOutL = 44,
        EyeLookOutR = 45,
        EyeWideR = 46,
        EyeWideL = 47,
        MouthRight = 48,
        MouthDimpleL = 49,
        MouthLowerDownL = 50,
        TongueShapeOut = 51,
        VisemePP = 52,
        VisemeCH = 53,
        Visemeo = 54,
        VisemeO = 55,
        VisemeI = 56,
        Visemeu = 57,
        VisemeRR = 58,
        VisemeXX = 59,
        Visemeaa = 60,
        Visemei = 61,
        VisemeFF = 62,
        VisemeU = 63,
        VisemeTH = 64,
        Visemekk = 65,
        VisemeSS = 66,
        Visemee = 67,
        VisemeDD = 68,
        VisemeE = 69,
        Visemenn = 70,
        Visemesil = 71,
        FaceMax = 72,
    };

    public class PicoFaceTracking
    {
        public static void SetFacePicoParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read((int)FaceMax);

            eye.Right.Openness = 1.0f - Math.Clamp(p[EyeBlinkR] + p[EyeBlinkR] * p[EyeSquintR], 0.0f, 1.0f);
            eye.Left.Openness = 1.0f - Math.Clamp(p[EyeBlinkL] + p[EyeBlinkL] * p[EyeSquintL], 0.0f, 1.0f);

            #region Eye Expressions

            w[EyeSquintRight] = p[EyeSquintR];
            w[EyeSquintLeft] = p[EyeSquintL];
            w[EyeWideRight] = p[EyeWideR];
            w[EyeWideLeft] = p[EyeWideL];

            #endregion

            #region Eyebrow Expressions

            w[BrowPinchRight] = p[BrowDownR];
            w[BrowPinchLeft] = p[BrowDownL];
            w[BrowLowererRight] = p[BrowDownR];
            w[BrowLowererLeft] = p[BrowDownL];
            w[BrowInnerUpRight] = p[BrowInnerUp];
            w[BrowInnerUpLeft] = p[BrowInnerUp];
            w[BrowOuterUpRight] = p[BrowOuterUpR];
            w[BrowOuterUpLeft] = p[BrowOuterUpL];

            #endregion

            #region Cheek Expressions

            w[CheekSquintRight] = p[CheekSquintR];
            w[CheekSquintLeft] = p[CheekSquintL];
            float mouthLeft = p[MouthLeft];
            float mouthRight = p[MouthRight];
            float diffThreshold = 0.1f;

            if (mouthLeft > mouthRight + diffThreshold)
            {
                w[CheekPuffLeft] = p[CheekPuff];
                w[CheekPuffRight] = p[CheekPuff] + p[MouthLeft];
            }
            else if (mouthRight > mouthLeft + diffThreshold)
            {
                w[CheekPuffLeft] = p[CheekPuff] + p[MouthRight];
                w[CheekPuffRight] = p[CheekPuff];
            }
            else
            {
                w[CheekPuffLeft] = p[CheekPuff];
                w[CheekPuffRight] = p[CheekPuff];
            }

            #endregion

            #region Jaw Exclusive Expressions

            w[JawOpen] = p[JawShapeOpen];
            w[JawRight] = p[JawShapeRight];
            w[JawLeft] = p[JawShapeLeft];
            w[JawForward] = p[JawShapeForward];
            w[MouthClosed] = p[MouthClose];

            #endregion

            #region Lip Expressions

            w[LipSuckUpperRight] = p[MouthRollUpper];
            w[LipSuckUpperLeft] = p[MouthRollUpper];
            w[LipSuckLowerRight] = p[MouthRollLower];
            w[LipSuckLowerLeft] = p[MouthRollLower];

            var isFunnelRight = p[MouthPucker] > 0.3f && p[MouthPressR] < 0.2f;
            var isFunnelLeft = p[MouthPucker] > 0.3f && p[MouthPressL] < 0.2f;
            var mouthFunnelFixed = p[MouthPucker];

            w[LipFunnelUpperRight] = isFunnelRight ? mouthFunnelFixed : p[MouthFunnel];
            w[LipFunnelUpperLeft] = isFunnelLeft ? mouthFunnelFixed : p[MouthFunnel];
            w[LipFunnelLowerRight] = isFunnelRight ? mouthFunnelFixed : p[MouthFunnel];
            w[LipFunnelLowerLeft] = isFunnelLeft ? mouthFunnelFixed : p[MouthFunnel];

            w[LipPuckerUpperRight] = p[MouthPucker];
            w[LipPuckerUpperLeft] = p[MouthPucker];
            w[LipPuckerLowerRight] = p[MouthPucker];
            w[LipPuckerLowerLeft] = p[MouthPucker];

            w[MouthUpperUpRight] = Math.Max(0, p[MouthUpperUpR] - p[NoseSneerR]);
            w[MouthUpperUpLeft] = Math.Max(0, p[MouthUpperUpL] - p[NoseSneerL]);
            w[MouthUpperDeepenRight] = Math.Max(0, p[MouthUpperUpR] - p[NoseSneerR]);
            w[MouthUpperDeepenLeft] = Math.Max(0, p[MouthUpperUpL] - p[NoseSneerL]);

            w[NoseSneerRight] = p[NoseSneerR];
            w[NoseSneerLeft] = p[NoseSneerL];

            w[MouthLowerDownRight] = p[MouthLowerDownR];
            w[MouthLowerDownLeft] = p[MouthLowerDownL];

            w[MouthUpperRight] = p[MouthRight];
            w[MouthUpperLeft] = p[MouthLeft];
            w[MouthLowerRight] = p[MouthRight];
            w[MouthLowerLeft] = p[MouthLeft];

            float smileScale = 2.0f;

            var mouthSmileLeft = (p[MouthSmileL] - (p[JawShapeOpen] > 0.1f ? p[MouthRollLower] : 0f)) * smileScale;
            w[MouthCornerPullLeft] = p[MouthRollLower] < 0.25f ? mouthSmileLeft : 0f;
            w[MouthCornerSlantLeft] = p[MouthRollLower] < 0.25f ? mouthSmileLeft - (p[MouthRollLower] * smileScale) : 0f;

            var mouthSmileRight = (p[MouthSmileR] - (p[JawShapeOpen] > 0.1f ? p[MouthRollLower] : 0f)) * smileScale;
            w[MouthCornerPullRight] = p[MouthRollLower] < 0.25f ? mouthSmileRight : 0f;
            w[MouthCornerSlantRight] = p[MouthRollLower] < 0.25f ? mouthSmileRight - (p[MouthRollLower] * smileScale) : 0f;

            var mouthFrownRight = p[MouthFrownR];
            w[MouthFrownRight] = p[JawShapeOpen] > 0.1f
                ? mouthFrownRight / 2f
                : p[MouthRollLower] > 0.25f
                    ? mouthFrownRight * 2.5f + p[MouthRollLower]
                    : mouthFrownRight;
            var mouthFrownLeft = p[MouthFrownL];
            w[MouthFrownLeft] = p[JawShapeOpen] > 0.1f
                ? mouthFrownLeft / 2f
                : p[MouthRollLower] > 0.25f
                    ? mouthFrownLeft * 2.5f + p[MouthRollLower]
                    : mouthFrownLeft;

            w[MouthStretchRight] = p[MouthStretchR];
            w[MouthStretchLeft] = p[MouthStretchL];

            w[MouthDimpleRight] = p[MouthDimpleR];
            w[MouthDimpleLeft] = p[MouthDimpleL];

            w[MouthRaiserUpper] = p[MouthShrugUpper];
            w[MouthRaiserLower] = p[MouthShrugLower];
            w[MouthPressRight] = p[MouthPressR];
            w[MouthPressLeft] = p[MouthPressL];

            #endregion

            #region Tongue Expressions

            w[TongueOut] = p[TongueShapeOut] > 0.9f ? p[TongueShapeOut] : 0f;

            #endregion
        }
    }
}
