using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public HUDFinalController hudFinal;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Si no se asigno por inspector, se busca en la escena
        if (hudFinal == null)
        {
            hudFinal = Object.FindFirstObjectByType<HUDFinalController>();
        }

        if (hudFinal != null)
        {
            hudFinal.ShowFinalHUD();

            // Opcional: desactivar movimiento del jugador
            // PlayerMovement pm = other.GetComponent<PlayerMovement>();
            // if (pm != null)
            // {
            //     pm.enabled = false;
            // }
        }
    }
}
