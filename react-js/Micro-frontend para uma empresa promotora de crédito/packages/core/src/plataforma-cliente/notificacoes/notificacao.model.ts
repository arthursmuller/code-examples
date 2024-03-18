export enum NotificacaoSeveridadeEnum {
  baixa = 0,
  media = 1,
  urgente = 2,
  gravissima = 3,
}

export interface Notificacao {
  id: number;
  titulo?: string;
  descricao?: string;
  urlReferencia?: string;
  dataNotificacao: string;
  dataVisualizacao: string;
  severidade: NotificacaoSeveridadeEnum;
}
