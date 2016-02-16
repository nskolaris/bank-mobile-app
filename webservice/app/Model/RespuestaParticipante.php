<?php
class RespuestaParticipante extends AppModel {
	var $name = 'RespuestaParticipante';
	
	public $belongsTo = array(
        'Participante' => array(
            'className' => 'Participante',
            'foreignKey' => 'participante_id'
        )
    );
?>