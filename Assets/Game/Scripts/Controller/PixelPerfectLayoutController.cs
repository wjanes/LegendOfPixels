

//#define DEBUG_PIXELPERFECTLAYOUTCONTROLLER


using UnityEngine;
using UnityEngine.UI;


// License:
// The MIT License (MIT)
// 
// Copyright (c) 2017 René Bühling, www.gamedev-profi.de
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

/// <summary>
/// Skaliert ein Container-UI-Element so, dass es die Fläche der Kamera *inklusive* einer benutzerdefinierten
/// Projektionsmatrix einnimmt und zudem auf ganze Pixel rundet. Dadurch ist es möglich, UI-Komponenten 
/// auch bei Verwendung eines strengen PixelArt-Rasters, z.B. mit Hilfe von GGEZ Perfect Pixel Camera, 
/// an den Bildschirmrändern auszurichten.
/// 
/// Verwendung: 
/// - UI-Hierarchie für Pixel-Art wie im Video-Kurs erklärt aufbauen.
/// - Dem Canvas ein UI-Element (z.B. Image) hinzufügen, das als Container für alle rand-ausgerichteten Elemente dient.
/// - Diesem Container dieses Script hinzufügen.
/// - Dem Container andere UI-Komponenten unterordnen und wie gewohnt mit dem Anchor/Pivot/usw. am gewünschten Rand ausrichten.
/// 
/// - Die Image-Komponente des Containers kann gelöscht oder deaktiviert werden, nur RectTransform, CanvasRenderer und PixelPerfectLayoutController sind erforderlich.
/// </summary>
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
[ExecuteInEditMode] //Erforderlich, um Update() im Editor auszuführen, was wiederum nötig ist, damit auch horizontale Fenstergrößenänderungen das Neuberechnen auslösen
public class PixelPerfectLayoutController : UnityEngine.EventSystems.UIBehaviour, ILayoutSelfController
{

    public virtual void SetLayoutHorizontal()
    {
        //Da UpdateRectTransform sowohl horizontal wie auch vertikal aktualisiert, genügt ein Aufruf durch SetLayoutVertical.

        //UpdateRectTransform();
    }

    public virtual void SetLayoutVertical()
    {
        UpdateRectTransform();
    }

#if UNITY_EDITOR
    protected override void OnValidate() //This tells when there is a change in the inspector
    {
        base.OnValidate();
#if DEBUG_PIXELPERFECTLAYOUTCONTROLLER
        Debug.Log("Validate");
#endif
        setDirty();
    }
#endif
    

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        setDirty();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        setDirty();
    }

    /// <summary>
    /// Löst ein Neuzeichnen des Layouts bei der nächsten vom System dafür vorgesehenen Möglichkeit aus.
    /// Das Wiederum wird SetLayoutHorizontal/Vertical auslösen.
    /// </summary>
    protected void setDirty()
    {
        if (!isActiveAndEnabled) return;
        LayoutRebuilder.MarkLayoutForRebuild(myRT);
    }

    private RectTransform _pRT;

    /// <summary>
    /// Das übergeordnete Canvas-Element enthält die volle Bildauflösung.
    /// </summary>
    protected RectTransform pRT
    {
        get
        {

            if (_pRT == null) _pRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            return _pRT;

        }
    }

    private RectTransform _myRT;

    /// <summary>
    /// Mein RectTransform ist das zu verändernde Container-Objekt.
    /// </summary>
    protected RectTransform myRT
    {
        get
        {
            if (_myRT == null) _myRT = this.GetComponent<RectTransform>();
            return _myRT;
        }
    }

    /// <summary>
    /// Cache für die bisherige Bildschirmauflösung. 
    /// Erforderlich, um Änderungen zu erkennen und ein Change-Ereignis zu in Update() zu simulieren.
    /// </summary>
    private float oldScreenW = 0;
    /// <summary>
    /// Cache für die bisherige Bildschirmauflösung. 
    /// Erforderlich, um Änderungen zu erkennen und ein Change-Ereignis in Update() zu simulieren.
    /// </summary>
    private float oldScreenH = 0;

    private void Update()
    {
        if (Screen.width != oldScreenW || Screen.height != oldScreenH) //Im Editor werden horizontale Fenstergrößenänderungen, im Player sogar beide, u.U. nicht erkannt. Daher diese manuelle Simulation eines Resize-Events.
        {
#if DEBUG_PIXELPERFECTLAYOUTCONTROLLER
            Debugger.debugStr2 = "Update from old "+oldScreenW+","+oldScreenH;
#endif
            setDirty();
            oldScreenW = Screen.width;
            oldScreenH = Screen.height;
        }
#if DEBUG_PIXELPERFECTLAYOUTCONTROLLER
        if (Application.isPlaying && Input.GetKeyUp(KeyCode.F5))
        {
            Debugger.debugStr2 = "Update";
            UpdateRectTransform();
        }
#endif
    }


    /// <summary>
    /// Ermittelt den Kamerasichtbereich inklusive Anwendung der Projektionsmatrix.
    /// Dies entspricht dem tatsächlich gerenderten Kamerabild.
    /// </summary>
    /// <param name="camera">Zu verwendende Kamera, in der Regel Camera.main.</param>
    /// <returns>Vier Eckpunkte des Sichtfelds in Weltkoordinaten.</returns>
    private Vector3[] getScaledWorldView(Camera camera) //debug: red
    {
        Vector3[] far = new Vector3[4];
        far[0] = new Vector3(0.0f, 0.0f, camera.farClipPlane); 
        far[1] = new Vector3(0.0f, 1f, camera.farClipPlane);
        far[2] = new Vector3(1f, 1f, camera.farClipPlane);
        far[3] = new Vector3(1f, 0.0f, camera.farClipPlane);

        for (int index = 0; index < 4; ++index)
            far[index] = camera.ViewportToWorldPoint(far[index]);  //rechnet den unterschied aus, far[] zuvor für beide fälle gleich
        return far;
    }

    /// <summary>
    /// Ermittelt den Kamerasichtbereich ohne Projektionsmatrix.
    /// Dies entspricht mehr oder weniger der Canvas-Size (bei Kamera-gebundenem Rendermode).
    /// </summary>
    /// <param name="camera">Zu verwendende Kamera, in der Regel Camera.main.</param>
    /// <returns>Vier Eckpunkte des Sichtfelds in Weltkoordinaten.</returns>
    private Vector3[] getUnscaledWorldView(Camera camera) //debug: yellow
    {
        Matrix4x4 m = camera.projectionMatrix;
        camera.ResetProjectionMatrix();

        Vector3[] far = new Vector3[4];
        far[0] = new Vector3(0.0f, 0.0f, camera.farClipPlane);
        far[1] = new Vector3(0.0f, 1f, camera.farClipPlane);
        far[2] = new Vector3(1f, 1f, camera.farClipPlane);
        far[3] = new Vector3(1f, 0.0f, camera.farClipPlane);
        for (int index = 0; index < 4; ++index)
            far[index] = camera.ViewportToWorldPoint(far[index]); //rechnet den unterschied aus, far[] zuvor für beide fälle gleich
        camera.projectionMatrix = m;

        return far;
    }

    

    /// <summary>
    /// Runde auf PixelArt-Pixel.
    /// </summary>
    /// <param name="f">Zahl, die gerundet werden soll.</param>
    /// <returns>integer, eingerastet im PixelArt-Raster.</returns>
    protected int floorToInt(float f, float frac)
    {
        return Mathf.RoundToInt(Mathf.Floor(f / frac) * frac);
    }


    /// <summary>
    /// Aktualisiert die Größe des Objekts anhand der aktuellen Bildgröße.
    /// </summary>
    public void UpdateRectTransform()
    {
        if (!enabled) return;

        //Sichtbare Bereiche ermitteln:
        Camera camera = Camera.main; // FindObjectOfType<Camera>();
        Vector3[] inner = getScaledWorldView(camera); //Kamera-Sichtfeld (durch die Matrix verkleinert)
        Vector3[] outer = getUnscaledWorldView(camera); //Canvas-Sichtfeld (in voller Kameragröße ohne Matrix)

        //Skalierungsfaktor aus dem Unterschied der beiden berechnen:
        float factorX = (inner[2].x-inner[0].x) / (outer[2].x-outer[0].x); //wichtig: Die Ausdehung (Breiten) müssen verglichen werden, es genügt nicht, die Eckpunkte (inner[0].x/outer[0].x) zu vergleichen, denn dann ist das Ergebnis abhängig von der Position der Kamera im Raum und liefert falsche Ergebnisse.
        float factorY = (inner[2].y-inner[0].y) / (outer[2].y-outer[0].y);


#if RECHENWEG_MIT_PADDING
        // Dicke des Rands in Pixeln berechnen, in dem von der vollen Größe des Canvas die nach gleichem Verhältnis runterskalierte Größe des Canvas abgezogen wird.
        float paddingX = (pRT.rect.width - (pRT.rect.width * factorX)); //Dicke des Rands horizontal (= gesamte Dicke! also links und rechts ist der Rand jeweils halb so dick)
        float paddingY = (pRT.rect.height - (pRT.rect.height * factorY));
        myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,   floorToInt(pRT.rect.width  - paddingX,  2f)); // auf 2 pixel einrasten = nötig, da wir nach oben und unten je auf ein ganzes pixel einrasten wollen 
        myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,     floorToInt(pRT.rect.height - paddingY, 2f)); 
        //myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, roundTo(pRT.rect.height - paddingY * 2, 16f)); //Alternative: auf ganze 16er-Kacheln runden
#else //Rechenweg direkt
    #if DEBUG_PIXELPERFECTLAYOUTCONTROLLER
        Debugger.debugStr = pRT.name + "=" + pRT.rect.ToString() + " inner=" + inner[0] + "," + inner[1] + "," + inner[2] + "," + inner[3] + " outer=" + outer[0] + "," + outer[1] + "," + outer[2] + "," + outer[3]+" fac:"+factorX+","+factorY;
    #endif
        myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, floorToInt(pRT.rect.width * factorX, 2f)); // auf 2 pixel einrasten = nötig, da wir nach oben und unten je auf ein ganzes pixel einrasten wollen 
        myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, floorToInt(pRT.rect.height * factorY, 2f));
#endif

        //Warnung: Setze die Größe hier nicht mehrfach hintereinander, sondern nur einmal (setSize...) pro Richtung, denn sonst wird Unity crashen.

    }
       

}
