import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export interface Evento {
    eventoId : number;
    local : string;
    dataEvent : Date;
    tema : string;
    qtdPessoas : number;
    imagemURL : string;
    telefone : string;
    email : string;
    lote : Lote[];
    redesSociais : RedeSocial[];
    palestrantesEventos : Palestrante[];
}
