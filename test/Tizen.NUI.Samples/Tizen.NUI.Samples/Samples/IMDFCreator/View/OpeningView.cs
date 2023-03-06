using Space.BuildingTool.ProceduralMesh;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class OpeningView : IMDF3DView
    {
        private ModelRenderer meshRenderer;
        public OpeningView(Opening opening)
        {
            Size = new Size(300, 300, 300);
            Position = new Position(0, 0, 0);

            CreateMesh(opening);
        }
        private void CreateMesh(Opening opening)
        {
            Color color = null;
            if(opening.Category == OpeningCategory.DOOR)
            {
                var meshDraft = MVMeshCreator.Instance.CreateOpeningMeshInfo(opening.Coordinates[0], opening.Coordinates[1], opening.Category);
                meshRenderer = new ModelRenderer();
                color = new Color(0.66f, 0.35f, 0.0f, 1.0f);
                AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, null, color));
            }
            else
            {
                color = new Color(0.2f, 0.2f, 0.7f, 1.0f);
            }
        }
    }
}
