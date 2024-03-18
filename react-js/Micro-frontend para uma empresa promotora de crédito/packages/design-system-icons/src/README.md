# Esse diretório é gerenciado por um script. Não efetuar alterações manuais aqui.

## Scripts disponíveis

`$ yarn run replace-all-icons`

O comando acima, irá apagar todos os svgs e irá realizar o download de todos novamente.


`$ yarn run update-icons`

O comando acima, irá apenas realizar o download dos svgs com nome diferente dos existentes.


Observações:

  1 - Ao rodar o replace-all-icons é comum receber o erro HTTP 429, devido ao grande número de requests no figma.
      Portanto, após rodar um replace-all-icons aguarde 5 minutos e rode um yarn run update-icons para realizar o download dos svgs restantes.

  2 - Usar decodeURIComponent do frame ?node-id=2988%3A59070 para popular a variáve REACT_APP_FRAME_WITH_ICONS_ID=1768:5444


fonte: https://levelup.gitconnected.com/learn-svg-to-react-using-figma-api-be0a5f9c0ca

    https://github.com/vborodulin/how-to-deliver-svg-icons-to-react
