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
            var meshDraft = MVMeshCreator.Instance.CreateOpeningMeshInfo(opening.Coordinates[0], opening.Coordinates[1], opening.Category);

            meshRenderer = new ModelRenderer();
            Color color = null;
            if(opening.Category == OpeningCategory.DOOR)
            {
                color = new Color(0.7f, 0.2f, 0.2f, 0.7f);
            }
            else
            {
                color = new Color(0.2f, 0.2f, 0.75f, 0.7f);
            }
            AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, "/images/IMDFTextures/s_baseColor.jpeg", color));
        }
    }
}
