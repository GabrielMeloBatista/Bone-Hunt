using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Merchant : MonoBehaviour
{
    [SerializeField] GameController gameController;
    VisualElement root;

    public void changeShowStore()
    {
        gameController.HandleStore();
        updateVisible();
    }

    public void HandleStore()
    {
        if (gameController.getIsShopping())
        {
            updateVisible();
        }
    }

    void updateVisible()
    {
        root.visible = gameController.getIsShopping();
        root.SetEnabled(gameController.getIsShopping());
    }

    private void OnEnable()
    {
        gameController = GameController.getInstance();
        root = GetComponent<UIDocument>().rootVisualElement;
        updateVisible();

        // Define os item vendidos na loja
        var items = new List<string>
            {
                "Axe",
                "Picaxe",
                "Whip",
                "Cool Hat"
            };

        // Este Label e o texto que aparece no UI TOOLKIT
        Func<VisualElement> makeItem = () => new Label();

        // Este pega os items e coloca no Label(elemento html)
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];

        // Pega a raiz da pagina

        // Define o tamanho da coluna
        var itemHeight = 30;

        // Busca os botões e o container
        Button buy = root.Q<Button>("Buy");
        Button close = root.Q<Button>("Close");
        VisualElement visualElement = root.Q<VisualElement>("Container");

        // Cria uma lista, e nela já colocoa os itens dela
        var listView = new ListView(items, itemHeight, makeItem, bindItem);

        // Define o tipo de seleção da lista
        listView.selectionType = SelectionType.Multiple;

        // Adiciona um flexGrow na lista
        listView.style.flexGrow = 1.0f;

        // Adiciona no container a lista criada no codigo
        visualElement.Add(listView);

        // Coloca uma função para o botão de fechar
        close.clicked += () => changeShowStore();
        // Coloca outra função para o botão de fechar
        close.clicked += () => gameController.ResumeGame();

        // Adiciona uma função para o botão de comprar
        buy.clicked += () => Debug.Log("Buy: " + listView.selectedItem);

    }
}
