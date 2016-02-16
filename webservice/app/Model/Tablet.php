<?php
class Tablet extends AppModel {
	var $name = 'Tablet';
	
	public $validate = array(
		'nombre' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar un nombre.'
		),
		'code' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar el codigo.'
		)
    );
	
	/* Funciones */
	
	function getByCode($code){
		$tablet = $this->find('first',array('conditions'=>array('Tablet.code'=>$code,'Tablet.deleted'=>null)));
		return $tablet;
	}
	
	function getAll(){
		return $this->find('all',array());
	}
	
	function afterFind($results, $primary = false){
		foreach($results as $key => $val){
			if(isset($val['Tablet'])){
				if($val['Tablet']['deleted'] != null){
					unset($results[$key]);
				}
			}
		}
		return $results;
	}
	
	function agregar($data){
		$response = array('status'=>false,'message'=>'No se pudo agregar la tablet');
		if(!$this->getByCode($data['Tablet']['code'])){
			$this->create();
			if($this->save($data)){
				$response['status'] = true;
				$response['message'] = 'Se agregó la tablet con éxito';
			}
		}else{
			$response['message'] = 'El código especificado ya existe';
			$this->validationErrors['code'] = 'El código especificado ya existe';
		}
		return $response;
	}
	
	function borrar($id){
		$this->id = $id;
		return $this->saveField('deleted',date('Y-m-d h:i:s'));
	}
}
?>