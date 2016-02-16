<?php
class Participante extends AppModel {
	var $name = 'Participante';
	
	public $encryptedFields = array('nombre','apellido','email','telefono','dni','provincia');
	
	var $belongsTo = array(
		'Evento' => array(
			'className' => 'Evento',
			'foreignKey' => 'evento_id',
		),
		'Promotora' => array(
			'className' => 'Usuario',
			'foreignKey' => 'promotora_id',
		)
	);

	/* Funciones */

	function getByCodeEmail($code,$email){
		if($evento_existe = $this->Evento->getByCode($code)){
			return $this->find('first',array('conditions'=>array('Participante.evento_id'=>$evento_existe['Evento']['id'],'Participante.email'=>$email)));
		}else{
			return false;
		}
	}
	
	function beforeSave($options = array()){
		App::uses('Security', 'Utility');
		if(!empty($this->data['Participante']['evento_code'])){
			if($evento_existe = $this->Evento->getByCode($this->data['Participante']['evento_code'])){
				$this->data['Participante']['evento_id'] = $evento_existe['Evento']['id'];
			}else{
				return false;
			}
		}
		foreach($this->encryptedFields as $fieldName){
			if(!empty($this->data[$this->alias][$fieldName])){
				$this->data[$this->alias][$fieldName] = Security::encrypt($this->data[$this->alias][$fieldName], Configure::read('Security.key'));
			}
		}
		return true;
	}
	
	public function afterFind($results, $primary = false){
		App::uses('Security', 'Utility');
		foreach($this->encryptedFields as $fieldName){
			if(!isset($results[$this->alias])){
				foreach($results as $i => $data){
					if(!empty($data[$this->alias][$fieldName])){
						$results[$i][$this->alias][$fieldName] = Security::decrypt($data[$this->alias][$fieldName], Configure::read('Security.key'));
					}
				}
			}
			if(!empty($results[$this->alias][$fieldName])){
				$results[$this->alias][$fieldName] = Security::decrypt($results[$this->alias][$fieldName], Configure::read('Security.key'));
			}
		}
		return $results;
	}
	
	function agregar($data){
		$response = array('status'=>false,'message'=>'No se pudo agregar el participante','errors'=>null);
		$this->set($data);
		if($this->validates()){$this->create();}
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se agregó el participante con éxito';
		}
		return $response;
	}
}
?>