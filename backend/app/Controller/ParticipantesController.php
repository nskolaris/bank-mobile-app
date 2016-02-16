<?php
App::uses('AppController', 'Controller');

class ParticipantesController extends AppController{

	var $uses = array('Participante');
	public $components = array('Paginator');
	
	function index(){
		if(!empty($this->data)){
			$this->Paginator->settings['conditions'] = array();
			$url_conditions = array();
			foreach($this->data as $model => $condition){
				foreach($condition as $field => $value){
					if($value != ''){
						if($field == 'fecha_desde'){
							$this->Paginator->settings['conditions']['Participante.fecha_ingresado >='] = $value;
						}elseif($field == 'fecha_hasta'){
							$this->Paginator->settings['conditions']['Participante.fecha_ingresado <='] = $value;
						}else{
							$this->Paginator->settings['conditions'][$model.'.'.$field] = $value;
						}
						$url_conditions[$field] = $value;
					}
				}
			}
			$this->Session->write('participantes_filtro',$this->Paginator->settings['conditions']);
			$this->set('url_conditions',$url_conditions);
		}
		$this->set('eventos',$this->Participante->Evento->getCombo());
		$this->set('data',$this->Paginator->paginate('Participante'));
	}
	
	function exportar(){
		header('Content-type: application/ms-excel');
		header('Content-Disposition: attachment; filename=infoapptable.csv');

		$fp = fopen("php://output", "w");

		$conditions = $this->Session->read('participantes_filtro');
		$participantes = $this->Participante->find('all',array('conditions'=>$conditions));
		
		$td = array('Nombre','Apellido','E-mail','Teléfono','DNI','Provincia','Acepta beneficios?');
		fputcsv($fp, $td);
		
		foreach($participantes as $participante){
			$td = array();
			$td [] = $participante['Participante']['nombre'];
			$td [] = $participante['Participante']['apellido'];
			$td [] = $participante['Participante']['email'];
			$td [] = $participante['Participante']['telefono'];
			$td [] = $participante['Participante']['dni'];
			$td [] = $participante['Participante']['provincia'];
			$td [] = ($participante['Participante']['acepta_beneficios']==1?'Si':'No');
			fputcsv($fp, $td);
		}

		fclose($fp);
		exit;
	}

	function sincronizar(){
		$post = json_decode($_POST['data']);
		if(!empty($post)){
			$array_guardados = array();
			foreach($post as $participante){
				$participante = (array)$participante;
				if(!$existe = $this->Participante->getByCodeEmail($participante['evento_code'],$participante['email'])){
					$response = $this->Participante->agregar(array('Participante'=>$participante));
					if($response['status']){
						$array_guardados[$participante['local_id']] = $this->Participante->id;
					}
				}else{
					$array_guardados[$participante['local_id']] = $existe['Participante']['id'];
				}
			}
			echo json_encode($array_guardados);
		}
		exit;
	}
	
	function test_agregar(){
		if($this->Participante->agregar(array('Participante'=>array('evento_code'=>'code1234','nombre'=>'nombre','apellido'=>'apellido','email'=>'email@asdf.com','telefono'=>'12341234','dni'=>'12345678','provincia'=>'Buenos Aires','acepta_beneficios'=>1,'promotora_id'=>1,'fecha_ingresado'=>'0000-00-00 00:00:00','juego_nombre'=>'Trivia')))){
			$this->redirect(array('controller'=>'participantes','action'=>'index'));
		}
		exit;
	}
}
?>