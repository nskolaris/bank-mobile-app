<?php
class Premio extends AppModel {
	var $name = 'Premio';
	
	public $belongsTo = array(
        'Participante' => array(
            'className' => 'Participante',
            'foreignKey' => 'participante_id'
        )
    );
?>